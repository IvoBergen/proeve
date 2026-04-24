using UnityEngine;

/// <summary>
/// Manages NPC positioning in the scene.
/// </summary>
public class NPCLocationManager : MonoBehaviour
{
    [Header("NPC References")]
    [SerializeField] private GameObject _firstNPC;
    [SerializeField] private GameObject _lastNPC;

    [Header("Waypoints")]
    [SerializeField] private Transform _npcPos;
    [SerializeField] private Transform _finalNpcPos;

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