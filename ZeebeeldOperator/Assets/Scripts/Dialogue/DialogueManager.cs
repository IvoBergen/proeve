using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameStateManager gameStateManager;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private float _dialogueTextSpeed;

    private Queue<string> _sentences;

    public bool Active;

    private void Awake()
    {
        _sentences = new Queue<string>();
        _dialogueUI.SetActive(false);
    }

    public void BeginDialogue(Dialogue dialogue, bool firstTime = true)
    {
        if (dialogue == null) return;

        string[] sentencesToUse = firstTime
            ? dialogue.firstTimeSentences
            : dialogue.repeatSentences;

        BeginDialogue(dialogue.name, sentencesToUse);
    }

    public void BeginDialogue(string name, string[] sentences)
    {
        gameStateManager.InDialogue();

        Active = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _dialogueUI.SetActive(true);
        _nameText.text = name;

        _sentences.Clear();
        foreach (string s in sentences)
            _sentences.Enqueue(s);

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
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_dialogueTextSpeed);
        }
    }

    private void EndDialogue()
    {
        Active = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _dialogueUI.SetActive(false);

        gameStateManager.exitDialouge();
    }
}