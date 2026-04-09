using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] public bool _movementDisabled;

    [Header("References")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _cameraAnchor;

    [Header("Sensitivity")]
    [FormerlySerializedAs("_sensX")]
    [SerializeField] public float sensX = 100f;
    [FormerlySerializedAs("_sensY")]
    [SerializeField] public float sensY = 100f;

    [Header("Wall Clipping")]
    [SerializeField] private LayerMask _cameraCollisionMask = ~0;
    [SerializeField] private float _cameraProbeRadius = 0.08f;
    [SerializeField] private float _wallPadding = 0.02f;
    [SerializeField] private float _targetNearClip = 0.03f;

    private float _xRotation;
    private float _yRotation;
    private Camera _camera;
    private bool _cachedDefaultOffset;
    private Vector3 _defaultLocalOffset;

    private const float MinRayDistance = 0.0001f;
    private const float MinNearClip = 0.001f;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        if (_orientation == null && transform.parent != null)
            _orientation = transform.parent;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_movementDisabled == true)
            return;

        HandleMouseLook();
    }

    private void LateUpdate()
    {
        EnsureNearClip();
        ResolveWallClipping();
    }

    public void SetSensitivity(float value)
    {
        sensX = value;
        sensY = value;
        Debug.Log("Sensitivity set to: " + value);

    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        if (_orientation != null)
            _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }

    private void ResolveWallClipping()
    {
        Transform referenceTransform = _orientation != null ? _orientation : transform.parent;
        if (referenceTransform == null)
            return;

        Vector3 desiredPosition = GetDesiredCameraPosition(referenceTransform);
        Vector3 origin = referenceTransform.position;
        Vector3 toDesired = desiredPosition - origin;
        float distance = toDesired.magnitude;

        if (distance <= MinRayDistance)
        {
            transform.position = desiredPosition;
            return;
        }

        Vector3 direction = toDesired / distance;
        Vector3 resolvedPosition = desiredPosition;

        if (Physics.SphereCast(origin, _cameraProbeRadius, direction, out RaycastHit hit, distance, _cameraCollisionMask, QueryTriggerInteraction.Ignore))
        {
            float safeDistance = Mathf.Max(hit.distance - _wallPadding, 0f);
            resolvedPosition = origin + direction * safeDistance;
        }

        transform.position = resolvedPosition;
    }

    private Vector3 GetDesiredCameraPosition(Transform referenceTransform)
    {
        if (_cameraAnchor != null)
            return _cameraAnchor.position;

        if (!_cachedDefaultOffset)
        {
            _defaultLocalOffset = transform.localPosition;
            _cachedDefaultOffset = true;
        }

        return referenceTransform.TransformPoint(_defaultLocalOffset);
    }

    private void EnsureNearClip()
    {
        if (_camera == null)
            return;

        float targetNearClip = Mathf.Max(_targetNearClip, MinNearClip);
        if (!Mathf.Approximately(_camera.nearClipPlane, targetNearClip))
            _camera.nearClipPlane = targetNearClip;
    }
}
