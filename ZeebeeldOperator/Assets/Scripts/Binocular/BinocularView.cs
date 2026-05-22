using UnityEngine;

/// <summary>
/// Controls the movement of the binocular view based on mouse position, 
/// creating a parallax or panning effect within defined limits.
/// </summary>
public class BinocularView : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _sensitivity = 2.0f;
    [SerializeField] private float _limitX = 200f;
    [SerializeField] private float _limitY = 100f;
    [SerializeField] private float _smoothingSpeed = 5f;

    private Vector3 _startPosition;

    /// <summary>
    /// Stores the initial local position of the object.
    /// </summary>
    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    /// <summary>
    /// Calculates and interpolates the target position based on mouse movement.
    /// </summary>
    private void Update()
    {
        // Get mouse position relative to screen dimensions (-0.5 to 0.5 range)
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;

        // Calculate target position (moving the image in the opposite direction of the mouse)
        float targetX = _startPosition.x - (mouseX * _limitX * _sensitivity);
        float targetY = _startPosition.y - (mouseY * _limitY * _sensitivity);

        // Apply smooth interpolation to the target position
        Vector3 targetPosition = new Vector3(targetX, targetY, _startPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * _smoothingSpeed);
    }
}