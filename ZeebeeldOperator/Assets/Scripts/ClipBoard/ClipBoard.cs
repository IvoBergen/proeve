using bnyhtz;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages clipboard visibility and clue text rendering.
/// Toggles the clipboard with TAB, blocks opening during dialogue,
/// and routes open/close state through the GameStateManager.
/// </summary>
public class Clipboard : MonoBehaviour
{
    [SerializeField] private ClueManager _clueManager;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private KeyCode _toggleKey = KeyCode.Tab;
    [SerializeField] private bool _startOpen = false;
    [Header("Wall Clipping")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private LayerMask _clipboardCollisionMask = ~0;
    [SerializeField] private float _probeRadius = 0.08f;
    [SerializeField] private float _wallPadding = 0.02f;
    [SerializeField] private float _positionSmoothing = 18f;
    private GameObject _clipboardVisualRoot;

    private bool _isOpen;
    private Transform _selfTransform;
    private Collider[] _physicalColliders;
    private bool _hasCachedDefaultPose;
    private Vector3 _defaultLocalPosition;
    private Quaternion _defaultLocalRotation;

    private const float MinCastDistance = 0.0001f;
    private const float MinSmoothing = 0.01f;

    private void Awake()
    {
        _clipboardVisualRoot = gameObject;
        _selfTransform = transform;
        _physicalColliders = GetComponentsInChildren<Collider>(true);

        CacheDefaultLocalPose();
        DisablePhysicalClipboardColliders();

        if (_clueManager == null)
        {
            _clueManager = ClueManager.Instance;
        }

        if (_gameStateManager == null)
        {
            _gameStateManager = FindObjectOfType<GameStateManager>();
        }

        if (_dialogueManager == null)
        {
            _dialogueManager = FindObjectOfType<DialogueManager>();
        }
    }

    private void OnEnable()
    {
        if (_clueManager == null)
        {
            _clueManager = ClueManager.Instance;
        }

        if (_clueManager != null)
        {
            _clueManager.OnCluesChanged += RefreshText;
        }

        RefreshText();
    }

    private void Start()
    {
        SetClipboardState(_startOpen, true);
    }

    private void LateUpdate()
    {
        if (!_isOpen)
        {
            return;
        }

        ResolveWallClipping();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_toggleKey))
        {
            return;
        }

        bool wantsToOpen = !_isOpen;
        if (wantsToOpen && _dialogueManager != null && _dialogueManager.Active)
        {
            return;
        }

        SetClipboardState(wantsToOpen, false);
    }

    private void OnDisable()
    {
        if (_isOpen)
        {
            SetClipboardState(false, false);
        }

        if (_clueManager != null)
        {
            _clueManager.OnCluesChanged -= RefreshText;
        }
    }

    private void RefreshText()
    {
        if (_text == null)
        {
            return;
        }

        if (_clueManager == null)
        {
            _text.text = string.Empty;
            return;
        }

        var clues = _clueManager.GetAllClues();
        if (clues == null || clues.Count == 0)
        {
            _text.text = string.Empty;
            return;
        }

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < clues.Count; i++)
        {
            if (i > 0)
            {
                builder.AppendLine();
            }

            builder.Append("- ");
            builder.Append(clues[i]?.clueText ?? string.Empty);
        }

        _text.text = builder.ToString();
    }

    private void SetClipboardState(bool open, bool force)
    {
        if (!force && _isOpen == open)
        {
            return;
        }

        _isOpen = open;

        if (_clipboardVisualRoot != null && _clipboardVisualRoot != gameObject)
        {
            _clipboardVisualRoot.SetActive(open);
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(open);
            }
        }

        if (_gameStateManager != null)
        {
            if (open)
            {
                _gameStateManager.OpenClipboard();
            }
            else
            {
                _gameStateManager.CloseClipboard();
            }
        }

        if (!open)
        {
            RestoreDefaultLocalPose();
        }
    }

    private void ResolveWallClipping()
    {
        Transform cameraTransform = GetCameraTransform();
        if (cameraTransform == null)
        {
            return;
        }

        CacheDefaultLocalPose();
        Quaternion desiredRotation = GetDesiredWorldRotation();
        Vector3 desiredPosition = GetDesiredWorldPosition();
        Vector3 origin = cameraTransform.position;
        Vector3 toDesired = desiredPosition - origin;
        float distance = toDesired.magnitude;

        if (distance <= MinCastDistance)
        {
            ApplyResolvedPose(desiredPosition, desiredRotation);
            return;
        }

        Vector3 direction = toDesired / distance;
        Vector3 resolvedPosition = desiredPosition;
        RaycastHit[] hits = Physics.SphereCastAll(
            origin,
            _probeRadius,
            direction,
            distance,
            _clipboardCollisionMask,
            QueryTriggerInteraction.Ignore);

        float nearestDistance = float.PositiveInfinity;
        for (int i = 0; i < hits.Length; i++)
        {
            Collider hitCollider = hits[i].collider;
            if (hitCollider == null)
            {
                continue;
            }

            if (IsSelfOrChildCollider(hitCollider))
            {
                continue;
            }

            if (hits[i].distance < nearestDistance)
            {
                nearestDistance = hits[i].distance;
            }
        }

        if (!float.IsPositiveInfinity(nearestDistance))
        {
            float safeDistance = Mathf.Max(nearestDistance - _wallPadding, 0f);
            resolvedPosition = origin + direction * safeDistance;
        }

        ApplyResolvedPose(resolvedPosition, desiredRotation);
    }

    private void ApplyResolvedPose(Vector3 resolvedPosition, Quaternion desiredRotation)
    {
        float smoothing = Mathf.Max(_positionSmoothing, MinSmoothing);
        float t = 1f - Mathf.Exp(-smoothing * Time.deltaTime);

        _selfTransform.position = Vector3.Lerp(_selfTransform.position, resolvedPosition, t);
        _selfTransform.rotation = desiredRotation;
    }

    private bool IsSelfOrChildCollider(Collider hitCollider)
    {
        Transform hitTransform = hitCollider.transform;
        return hitTransform == _selfTransform || hitTransform.IsChildOf(_selfTransform);
    }

    private Transform GetCameraTransform()
    {
        if (_cameraTransform != null)
        {
            return _cameraTransform;
        }

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            _cameraTransform = mainCamera.transform;
        }

        return _cameraTransform;
    }

    private Vector3 GetDesiredWorldPosition()
    {
        if (_selfTransform.parent != null)
        {
            return _selfTransform.parent.TransformPoint(_defaultLocalPosition);
        }

        return _defaultLocalPosition;
    }

    private Quaternion GetDesiredWorldRotation()
    {
        if (_selfTransform.parent != null)
        {
            return _selfTransform.parent.rotation * _defaultLocalRotation;
        }

        return _defaultLocalRotation;
    }

    private void CacheDefaultLocalPose()
    {
        if (_hasCachedDefaultPose)
        {
            return;
        }

        _defaultLocalPosition = _selfTransform.localPosition;
        _defaultLocalRotation = _selfTransform.localRotation;
        _hasCachedDefaultPose = true;
    }

    private void RestoreDefaultLocalPose()
    {
        CacheDefaultLocalPose();
        _selfTransform.localPosition = _defaultLocalPosition;
        _selfTransform.localRotation = _defaultLocalRotation;
    }

    private void DisablePhysicalClipboardColliders()
    {
        if (_physicalColliders == null)
        {
            return;
        }

        for (int i = 0; i < _physicalColliders.Length; i++)
        {
            Collider col = _physicalColliders[i];
            if (col == null)
            {
                continue;
            }

            col.enabled = false;
        }
    }
}
