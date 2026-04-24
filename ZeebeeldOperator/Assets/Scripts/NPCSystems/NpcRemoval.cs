using UnityEngine;

/// <summary>
/// Responsable for removing the npc from the scene      
/// </summary>
public class NpcRemoval : MonoBehaviour
{
    public GameObject Npc;

    public void RemoveNPC()
    {
        Npc.SetActive(false);
    }
}
