
using UnityEngine;

public class Notes : MonoBehaviour, IInterface
{
    [Header("Note Content")]
    [TextArea(3, 10)]
    public string noteContent; // De tekst van de clue
    public bool isTheHint;

    public void Interact()
    {
        NoteUI ui = FindObjectOfType<NoteUI>();

        if (ui != null)
        {
            // We geven door of dit de hint was en wat de tekst is
            ui.OnPaperCollected(isTheHint, noteContent);
            Destroy(gameObject);
        }
    }
}
