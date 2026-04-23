using UnityEngine;

/// <summary>
/// Manages sonar effects, door states, and NPC positioning.
/// </summary>
public class SonarManager : MonoBehaviour
{
    [Header("Sonar Settings")]
    [SerializeField] private GameObject _sonarObject;
    [SerializeField] private Material _sonarMaterial;

    [Header("World Objects")]
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _firstNPC;
    [SerializeField] private GameObject _lastNPC;

    [Header("Waypoints")]
    [SerializeField] private Transform _npcPos;
    [SerializeField] private Transform _finalNpcPos;

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

    public void SetNpcActive()
    {
        if (_firstNPC != null && _finalNpcPos != null)
        {
            _firstNPC.transform.position = _finalNpcPos.position;
        }

        if (_lastNPC != null && _npcPos != null)
        {
            _lastNPC.transform.position = _npcPos.position;
        }
    }
}