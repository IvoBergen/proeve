using bnyhtz;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages clipboard visibility and clue text rendering.
/// Toggles the clipboard with TAB, blocks opening during dialogue,
/// and routes open/close state through the GameStateManager.
/// Resets to default clue view when closed to prevent soft-locks.
/// </summary>
public class Clipboard : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private ClueManager _clueManager;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private KeyCode _toggleKey = KeyCode.Tab;
    [SerializeField] private bool _startOpen = false;
    [SerializeField] private bool _guessSystemOpen;

    [Header("Wall Clipping")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private LayerMask _clipboardCollisionMask = ~0;
    [SerializeField] private float _probeRadius = 0.08f;
    [SerializeField] private float _wallPadding = 0.02f;
    [SerializeField] private float _positionSmoothing = 18f;
    [SerializeField] private float _hideIfCloserThan = 0.1f;
    [SerializeField] private bool _debugWallHideTransitions = true;

    [Header("Visuals")]
    [SerializeField] private GameObject _clipboardVisualRoot;
    [SerializeField] private GameObject _clipboardVisual;
    [SerializeField] private GameObject _differentClipboardUI;

    [Header("Events")]
    public UnityEvent guessSystemActive;
    public UnityEvent guessSystemDeactivated;

    #endregion

    #region Private Fields

    private bool _isOpen;
    private Transform _selfTransform;
    private Collider[] _physicalColliders;
    private bool _hasCachedDefaultPose;
    private bool _hiddenByWall;
    private bool _blockedByWall;
    private Collider _lastBlocker;
    private Vector3 _defaultLocalPosition;
    private Quaternion _defaultLocalRotation;

    private const float MinCastDistance = 0.0001f;
    private const float MinSmoothing = 0.01f;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _selfTransform = transform;
        _physicalColliders = GetComponentsInChildren<Collider>(true);

        CacheDefaultLocalPose();
        DisablePhysicalClipboardColliders();

        if (_clueManager == null) _clueManager = ClueManager.Instance;
        if (_gameStateManager == null) _gameStateManager = FindObjectOfType<GameStateManager>();
        if (_dialogueManager == null) _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnEnable()
    {
        if (_clueManager == null) _clueManager = ClueManager.Instance;
        if (_clueManager != null) _clueManager.OnCluesChanged += RefreshText;
        RefreshText();
    }

    private void Start()
    {
        SetClipboardState(_startOpen, true);
    }

    private void LateUpdate()
    {
        if (!_isOpen) return;
        ResolveWallClipping();
    }

    private void Update()
    {

        if (Input.GetKeyDown(_toggleKey))
        {
            bool wantsToOpen = !_isOpen;
            if (wantsToOpen && _dialogueManager != null && _dialogueManager.Active) return;

            SetClipboardState(wantsToOpen, false);
        }

        if (!_isOpen) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            _guessSystemOpen = !_guessSystemOpen;
            UpdateUIVisuals();
        }
    }

    private void OnDisable()
    {
        if (_isOpen) SetClipboardState(false, false);
        if (_clueManager != null) _clueManager.OnCluesChanged -= RefreshText;
    }

    #endregion

    #region Clipboard Logic

    private void SetClipboardState(bool open, bool force)
    {
        if (!force && _isOpen == open) return;

        _isOpen = open;
        _hiddenByWall = false;

        if (!open)
        {
            ResetToDefaultClueMode();
            RestoreDefaultLocalPose();
        }

        SetVisualsActive(open);

        if (_gameStateManager != null)
        {
            if (open) _gameStateManager.OpenClipboard();
            else _gameStateManager.CloseClipboard();
        }
    }

    private void ResetToDefaultClueMode()
    {
        _guessSystemOpen = false;
        UpdateUIVisuals();
    }

    private void UpdateUIVisuals()
    {
        if (_guessSystemOpen)
        {
            _clipboardVisual.SetActive(false);
            _differentClipboardUI.SetActive(true);

            guessSystemActive.Invoke();

        }
        else
        {

            _differentClipboardUI.SetActive(false);
            _clipboardVisual.SetActive(true);
            guessSystemDeactivated.Invoke();
        }
    }

    #endregion

    #region UI & Text Rendering

    private void RefreshText()
    {
        if (_text == null) return;
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
            if (i > 0) builder.AppendLine();
            builder.Append("- ").Append(clues[i]?.clueText ?? string.Empty);
        }
        _text.text = builder.ToString();
    }

    private void SetVisualsActive(bool active)
    {
        if (_clipboardVisualRoot != null) _clipboardVisualRoot.SetActive(active);
    }

    #endregion

    #region Wall Clipping Logic

    private void ResolveWallClipping()
    {
        Transform cameraTransform = GetCameraTransform();
        if (cameraTransform == null) return;

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
        RaycastHit[] hits = Physics.SphereCastAll(origin, _probeRadius, direction, distance, _clipboardCollisionMask, QueryTriggerInteraction.Ignore);

        float nearestDistance = float.PositiveInfinity;
        Collider nearestCollider = null;

        for (int i = 0; i < hits.Length; i++)
        {
            Collider hitCollider = hits[i].collider;
            if (hitCollider == null || IsSelfOrChildCollider(hitCollider)) continue;

            if (hits[i].distance < nearestDistance)
            {
                nearestDistance = hits[i].distance;
                nearestCollider = hitCollider;
            }
        }

        bool hasBlockingHit = !float.IsPositiveInfinity(nearestDistance);
        if (hasBlockingHit)
        {
            float safeDistance = Mathf.Max(nearestDistance - _wallPadding, 0f);
            resolvedPosition = origin + direction * safeDistance;
        }

        _blockedByWall = hasBlockingHit;
        _lastBlocker = hasBlockingHit ? nearestCollider : null;

        bool hideByWall = hasBlockingHit && (resolvedPosition - origin).sqrMagnitude <= _hideIfCloserThan * _hideIfCloserThan;
        SetHiddenByWall(hideByWall);

        if (!hideByWall) ApplyResolvedPose(resolvedPosition, desiredRotation);
    }

    private void ApplyResolvedPose(Vector3 resolvedPosition, Quaternion desiredRotation)
    {
        float smoothing = Mathf.Max(_positionSmoothing, MinSmoothing);
        float t = 1f - Mathf.Exp(-smoothing * Time.deltaTime);

        _selfTransform.position = Vector3.Lerp(_selfTransform.position, resolvedPosition, t);
        _selfTransform.rotation = desiredRotation;
    }

    private void SetHiddenByWall(bool hidden)
    {
        if (_hiddenByWall == hidden) return;
        _hiddenByWall = hidden;
        if (!_isOpen) return;
        SetVisualsActive(!hidden);
    }

    #endregion

    #region Helpers

    private bool IsSelfOrChildCollider(Collider hitCollider)
    {
        Transform hitTransform = hitCollider.transform;
        return hitTransform == _selfTransform || hitTransform.IsChildOf(_selfTransform);
    }

    private Transform GetCameraTransform()
    {
        if (_cameraTransform != null) return _cameraTransform;
        Camera mainCamera = Camera.main;
        if (mainCamera != null) _cameraTransform = mainCamera.transform;
        return _cameraTransform;
    }

    private Vector3 GetDesiredWorldPosition()
    {
        return (_selfTransform.parent != null) ? _selfTransform.parent.TransformPoint(_defaultLocalPosition) : _defaultLocalPosition;
    }

    private Quaternion GetDesiredWorldRotation()
    {
        return (_selfTransform.parent != null) ? _selfTransform.parent.rotation * _defaultLocalRotation : _defaultLocalRotation;
    }

    private void CacheDefaultLocalPose()
    {
        if (_hasCachedDefaultPose) return;
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
        if (_physicalColliders == null) return;
        foreach (var col in _physicalColliders)
        {
            if (col != null) col.enabled = false;
        }
    }

    #endregion
}