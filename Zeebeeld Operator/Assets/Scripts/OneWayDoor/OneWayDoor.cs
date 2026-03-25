using UnityEngine;
/// <summary>
/// Triggers a collider so that you cant moveback once you enter a room
/// </summary>
public class OneWayDoor : MonoBehaviour
{
    [SerializeField] Collider doorcollider;
    private void OnTriggerEnter(Collider other)
    {
        doorcollider.enabled = true;
    }
}
