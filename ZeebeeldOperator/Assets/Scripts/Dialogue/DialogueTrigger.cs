using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour, IInterface
{
    [Header("Dialogue Data")]
    public Dialogue dialogue;

    [SerializeField] private bool _hasTalked = false;

    [Header("Events")]
    public UnityEvent onFirstDialogueEnded;

    private DialogueManager _dialogueManager;
    private GameStateManager _gameStateManager;

    private bool _isOwner = false;
    private bool _waitingForFirstEnd = false;

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

        if (!_hasTalked)
        {
            _dialogueManager.BeginDialogue(dialogue, true);
            _waitingForFirstEnd = true;
            _hasTalked = true;
        }
        else
        {
            _dialogueManager.BeginDialogue(dialogue, false);
        }
    }

    private void Update()
    {
        if (!_isOwner || _dialogueManager == null) return;

        if (_dialogueManager.Active)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _dialogueManager.ShowNextLine();
            }
        }
        else
        {
            _isOwner = false;

            if (_waitingForFirstEnd)
            {
                onFirstDialogueEnded?.Invoke();
                _waitingForFirstEnd = false;
            }
        }
    }
}