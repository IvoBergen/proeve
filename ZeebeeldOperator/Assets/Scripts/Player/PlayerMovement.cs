using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Lets the player move with a small headbob
    /// </summary>
    #region References

    [Header("Variables")]
    public bool movementdisabled;

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _playerCamera;

    #endregion

    #region Movement Settings

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Headbob Settings")]
    [SerializeField] private float _bobFrequency = 5f;
    [SerializeField] private float _bobAmplitude = 0.1f;
    private float _bobTimer;
    private float _defaultYPos;

    [Header("Slope Settings")]
    [SerializeField] private float _maxSlopeAngle = 45f;
    [SerializeField] private float _playerHeight = 2f;

    #endregion

    #region Input
    private float _horizontalInput;
    private float _verticalInput;
    #endregion

    private Vector3 _moveDirection;

    private void Start()
    {
        _rb.freezeRotation = true;
        if (_playerCamera != null)
            _defaultYPos = _playerCamera.localPosition.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            bnyhtz.GameReset.ResetGame();
        }

        if (movementdisabled)
        {
            _horizontalInput = 0f;
            _verticalInput = 0f;
            _rb.velocity = Vector3.zero;
            return;
        }

        HandleInput();
        HandleHeadbob();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        HandleHeadbob();
        if (_moveDirection.magnitude > 0.1f)
        {
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
        }
    }

    private void HandleHeadbob()
    {
        if (_playerCamera == null) return;

        if (Mathf.Abs(_rb.velocity.magnitude) > 0.1f && _moveDirection.magnitude > 0.1f)
        {
            _bobTimer += Time.deltaTime * _bobFrequency;
            _playerCamera.localPosition = new Vector3(
                _playerCamera.localPosition.x,
                _defaultYPos + Mathf.Sin(_bobTimer) * _bobAmplitude,
                _playerCamera.localPosition.z
            );
        }
        else
        {
            _bobTimer = 0;
            Vector3 targetPos = new Vector3(_playerCamera.localPosition.x, _defaultYPos, _playerCamera.localPosition.z);
            _playerCamera.localPosition = Vector3.Lerp(_playerCamera.localPosition, targetPos, Time.deltaTime * 5f);
        }
    }
}