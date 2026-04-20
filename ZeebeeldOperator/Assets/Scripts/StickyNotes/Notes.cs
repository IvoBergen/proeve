
using UnityEngine;

public class Notes : MonoBehaviour, IInterface
{
    [Header("Note Data")]
    public string noteName = "Old Note";

    [TextArea(3, 10)]
    public string[] normalSentences; // De tekst voor de eerste 3 briefjes

    [TextArea(3, 10)]
    public string[] clueSentences;   // De tekst voor het allerlaatste briefje

    public void Interact()
    {
        NoteUI ui = FindObjectOfType<NoteUI>();

        // Alleen oppakken als de kapitein de quest heeft gestart
        if (ui != null && ui.IsQuestActive)
        {
            ui.OnPaperCollected(this);
        }
    }
}
