using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] public bool _movementDisabled;
    [Header("References")]
    [SerializeField] private Transform _orientation;

    [Header("Sensitivity")]
    [SerializeField] public float sensX = 100f;
    [SerializeField] public float sensY = 100f;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_movementDisabled == true)
            return;

        HandleMouseLook();
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
        _orientation.rotation = Quaternion.Euler(0f, _yRotation, 0f);
    }
}