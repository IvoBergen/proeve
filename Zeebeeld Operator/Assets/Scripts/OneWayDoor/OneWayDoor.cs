using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    [SerializeField] Collider doorcollider;
    private void OnTriggerEnter(Collider other)
    {
        doorcollider.enabled = true;
    }
}
