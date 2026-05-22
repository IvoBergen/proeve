using UnityEngine;

/// <summary>
/// Handles sonar activation and door state.
/// </summary>
public class SonarManager : MonoBehaviour
{
    [Header("Sonar Settings")]
    [SerializeField] private GameObject _sonarObject;
    [SerializeField] private Material _sonarMaterial;

    [Header("Door")]
    [SerializeField] private GameObject _door;

    public void TurnOnSonar()
    {
        if (_sonarObject != null && _sonarMaterial != null)
        {
            if (_sonarObject.TryGetComponent<Renderer>(out Renderer rend))
            {
                rend.material = _sonarMaterial;
            }
        }
    }

    public void OpenDoor()
    {
        if (_door != null)
        {
            _door.transform.rotation = Quaternion.Euler(270, 90, 0);
        }
    }
    public void CloseDoor()
    {
        _door.transform.rotation = Quaternion.Euler(270, 0, 0);
    }
}