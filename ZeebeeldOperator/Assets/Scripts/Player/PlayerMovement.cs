using UnityEngine;

/// <summary>
/// Handles player movement using Rigidbody physics.
/// The player can move based on input relative to an orientation transform.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    #region References
    [Header("variabels")]
    public bool movementdisabled;

    [Header("References")]

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _orientation;

    #endregion


    #region Movement Settings

    [Header("Movement Settings")]

    [SerializeField] private float _moveSpeed = 5f;

    [Header("Slope Settings")]

    [SerializeField] private float _maxSlopeAngle = 45f;
    [SerializeField] private float _playerHeight = 2f;

    #endregion


    #region Input

    private float _horizontalInput;
    private float _verticalInput;

    #endregion


    #region Movement

    private Vector3 _moveDirection;
    private RaycastHit _slopeHit;

    #endregion


    private void Start()
    {
        _rb.freezeRotation = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            bnyhtz.GameReset.ResetGame();
        }

        if (movementdisabled == true)
        {
            _horizontalInput = 0f;
            _verticalInput = 0f;
            _rb.velocity = Vector3.zero;
            return;
        }

        HandleInput();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }


    /// <summary>
    /// Reads player input from keyboard (WASD / Arrow keys).
    /// </summary>
    private void HandleInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }


    /// <summary>
    /// Applies movement force to the Rigidbody based on input direction.
    /// On slopes, projects movement along the surface and disables gravity
    /// to prevent slowdown.
    /// </summary>
    private void MovePlayer()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
    }

}