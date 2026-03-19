using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region References

    [Header("References")]

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _orientation;

    #endregion


    #region Movement Settings

    [Header("Movement Settings")]

    [SerializeField] private float _moveSpeed = 5f;

    #endregion


    #region Input

    private float _horizontalInput;
    private float _verticalInput;

    #endregion


    #region Movement

    private Vector3 _moveDirection;

    #endregion


    private void Start()
    {
        // Ensure the Rigidbody does not rotate when colliding
        _rb.freezeRotation = true;
    }


    private void Update()
    {
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
    /// Movement is relative to the orientation (camera direction).
    /// </summary>
    private void MovePlayer()
    {
        // Calculate movement direction relative to camera/orientation
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        // Apply force to move the player
        _rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
    }
}
