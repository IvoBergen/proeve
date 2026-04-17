using UnityEngine;
using UnityEngine.Events; // Required for UnityEvent

public class DialogueTrigger : MonoBehaviour, IInterface
{
    [Header("Dialogue Data")]
    public Dialogue dialogue;

    [Header("Events")]
    public UnityEvent onDialogueEnded; // Drag your NPC-specific functions here in the Inspector

    private DialogueManager _dialogueManager;
    private GameStateManager _gameStateManager;

    private bool _isOwner = false;

    private void Awake()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _gameStateManager = FindObjectOfType<GameStateManager>();
    }

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

        if (_dialogueManager.IsDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _dialogueManager.ShowNextLine();
            }
        }
        else
        {
            // --- The dialogue just ended ---
            _isOwner = false;

            // Trigger the NPC-specific event
            onDialogueEnded?.Invoke();
        }
    }
}