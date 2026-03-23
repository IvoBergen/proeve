using UnityEngine;

/// <summary>
/// Handles starting dialogue when interacting with an object.
/// Allows advancing dialogue with Space or Left Click.
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInterface
{
    [Header("Dialogue Data")]
    public Dialogue dialogue;

    private DialogueManager _dialogueManager;

    private void Awake()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        if (_dialogueManager == null)
            Debug.LogError("DialogueManager not found in the scene!");
    }

    /// <summary>
    /// Called when the player interacts with this object.
    /// Starts the dialogue.
    /// </summary>
    public void Interact()
    {
        if (_dialogueManager == null || dialogue == null) return;

        _dialogueManager.BeginDialogue(dialogue);
    }

    private void Update()
    {
        // Only allow advancing dialogue if it's active
        if (_dialogueManager != null && _dialogueManager.IsDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _dialogueManager.ShowNextLine();
            }
        }
    }
}