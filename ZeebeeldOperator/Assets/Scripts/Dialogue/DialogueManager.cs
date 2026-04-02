using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls dialogue flow, UI, and global dialogue state.
/// Does NOT store dialogue data.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent onDialogueStart;
    [SerializeField] private UnityEvent onDialogueEnd;
    [Header("variables")]
    [SerializeField] public bool Active;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private float _dialogueTextSpeed;

    private Queue<string> _sentences;

    public bool IsDialogueActive { get; private set; }

    private void Awake()
    {
        _sentences = new Queue<string>();

        _dialogueUI.SetActive(false);
        _nameText.text = "";
        _dialogueText.text = "";
    }

    public void BeginDialogue(Dialogue dialogue)
    {
        Active = true;
        onDialogueStart?.Invoke();
        IsDialogueActive = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _dialogueUI.SetActive(true);

        _nameText.text = dialogue.name;

        _sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(_sentences.Dequeue()));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";

        foreach (char letter in sentence)
        {
            yield return new WaitForSeconds(_dialogueTextSpeed);
            _dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        IsDialogueActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _dialogueUI.SetActive(false);
        onDialogueEnd?.Invoke();
        Active = false;
    }
}