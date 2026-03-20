using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// <c>DialogueManager</c> Handels the generation of the given dialogue and going to the next line(s).
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _dialogueText;

    private Queue<string> _sentences;


    private void Awake()
    {
        _sentences = new Queue<string>();
        _dialogueText.text = "";
        _nameText.text = "";
    }

    public void BeginDialogue(Dialogue dialogue)
    {
        _nameText.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        SpawnNextLine();
    }

    public void SpawnNextLine()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        _dialogueText.text = "";

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        _dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        Debug.Log("ended converstation");
    }
}
