using UnityEngine;

/// <summary>
/// <c>DialogueTrigger</c> Handles the start of the dialogue and being able to interact with it
/// </summary>
public class DialogueTrigger : MonoBehaviour, IInterface
{
    [Header("Components")]
    [SerializeField] private GameObject _dialogueUI;
    public Dialogue dialogue;

    private DialogueManager _dialogueManager;

    private void Awake()
    {
        // Fix 3: cache the reference instead of using FindObjectOfType every call
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Interact()
    {
        _dialogueUI.SetActive(true);
        TriggerDialogue();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Fix 2: advance to next line instead of restarting dialogue
            _dialogueManager.SpawnNextLine();
        }
    }

    public void TriggerDialogue()
    {
        _dialogueManager.BeginDialogue(dialogue);
    }
}