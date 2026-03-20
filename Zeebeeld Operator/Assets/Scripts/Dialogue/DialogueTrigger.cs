using UnityEngine;

/// <summary>
/// <c>DialogueTrigger</c> Handles the start of the dialogue and being able to interact with it
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInterface
{
    [Header("Components")]

    public Dialogue dialogue;

    public void Interact()
    {
        TriggerDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().BeginDialogue(dialogue);
    }

}
