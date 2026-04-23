using UnityEngine;

public class NpcRemoval : MonoBehaviour
{
    [SerializeField] GameObject NPC;

    public void RemoveNPC()
    {
        NPC.SetActive(false);
    }
}
