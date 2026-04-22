using UnityEngine;

public class Notes : MonoBehaviour, IInterface
{
    [Header("Note Data")]
    public string noteName = "Briefje";
    public Sprite normalImage;
    public Sprite clueImage;

    /// <summary>
    /// Checks if the quest is active and sends this note's data to the UI before it's collected.
    /// </summary>
    public void Interact()
    {
        NoteUI ui = FindObjectOfType<NoteUI>();

        if (ui != null && ui.IsQuestActive)
        {
            ui.OnPaperCollected(this);
        }
    }
}