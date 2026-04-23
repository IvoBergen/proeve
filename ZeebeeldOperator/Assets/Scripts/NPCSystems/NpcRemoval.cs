using UnityEngine;

/// <summary>
/// Responsable
/// </summary>
public class NpcRemoval : MonoBehaviour
{
    [SerializeField] public GameObject Npc;

    public void RemoveNPC()
    {
        Npc.SetActive(false);
    }
}
