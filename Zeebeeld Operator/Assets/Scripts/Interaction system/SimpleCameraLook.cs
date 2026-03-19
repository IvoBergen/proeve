using UnityEngine;

public class SimpleCameraLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 200f;
    [SerializeField] private float minY = -80f;
    [SerializeField] private float maxY = 80f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, minY, maxY);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}