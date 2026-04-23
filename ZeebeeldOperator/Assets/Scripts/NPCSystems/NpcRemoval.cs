using UnityEngine;

/// <summary>
/// Responsable for removing the npc from the scene      
/// </summary>
public class NpcRemoval : MonoBehaviour
{
    [SerializeField] public GameObject Npc;

    public void RemoveNPC()
    {
        Npc.SetActive(false);
    }
}
