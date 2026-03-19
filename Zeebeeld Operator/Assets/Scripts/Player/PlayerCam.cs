
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    #region References

    [Header("References")]

    [SerializeField] private Transform _orientation;

    #endregion


    #region Sensitivity

    [Header("Sensitivity")]

    [SerializeField] private float _sensX = 100f;
    [SerializeField] private float _sensY = 100f;

    #endregion


    #region Rotation

    private float _xRotation;
    private float _yRotation;

    #endregion


    private void Start()
    {
        // Lock and hide the cursor for FPS control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        HandleMouseLook();
    }


    /// <summary>
    /// Handles mouse input and rotates the camera accordingly.
    /// </summary>
    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensY;

        // Update rotation values
        _yRotation += mouseX;
        _xRotation -= mouseY;

        // Clamp vertical rotation so you can't flip
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // Apply rotation to camera
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0f);

        // Apply horizontal rotation to player orientation
        _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }
}
