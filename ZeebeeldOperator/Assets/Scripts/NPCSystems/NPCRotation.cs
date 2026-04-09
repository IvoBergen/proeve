using UnityEngine;

/// <summary>
/// <c>NPCRotation</c> Handles the rotation of the npc.
/// </summary>
public class NPCRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _alignmentThreshold = 2f; // degrees

    private NPCMovement _npcMovement;

    private void Awake()
    {
        _npcMovement = GetComponent<NPCMovement>();
    }

    private void Update()
    {
        if (_npcMovement.State != NPCState.Rotating) return;

        Vector3 direction = (_npcMovement.target - transform.position);
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);


        if (Quaternion.Angle(transform.rotation, targetRotation) < _alignmentThreshold)
            _npcMovement.SetMoving();
    }
}