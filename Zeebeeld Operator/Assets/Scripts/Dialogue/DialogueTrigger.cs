using UnityEngine;

/// <summary>
/// Handles starting dialogue when interacting with an object.
/// It gives the interacted object the owner bool so other objects dont interfere
/// Allows advancing dialogue with Space or Left Click.
/// Prevents dialogue start while the clipboard is open.
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInterface
{
    [Header("Dialogue Data")]
    public Dialogue dialogue;

    private DialogueManager _dialogueManager;
    private GameStateManager _gameStateManager;

    private bool _isOwner = false;

    private void Awake()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        if (_dialogueManager == null)
            Debug.LogError("DialogueManager not found in the scene!");

        _gameStateManager = FindObjectOfType<GameStateManager>();
        if (_gameStateManager == null)
            Debug.LogError("GameStateManager not found in the scene!");
    }

    /// <summary>
    /// Called when the player interacts with this object.
    /// Starts the dialogue.
    /// </summary>
    public void Interact()
    {
        if (_dialogueManager == null || dialogue == null) return;
        if (_gameStateManager != null && _gameStateManager.IsClipboardOpen) return;

        _isOwner = true;
        _dialogueManager.BeginDialogue(dialogue);
    }

    private void Update()
    {
        if (!_isOwner || _dialogueManager == null) return;

        // Only allow advancing dialogue if it's active
        if (_dialogueManager != null && _dialogueManager.IsDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _dialogueManager.ShowNextLine();
            }
        }
        else
        {
            _isOwner = false;
        }
    }
}
