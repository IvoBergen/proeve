using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// <c>DialogueManager</c> Handels the generation of the given dialogue and going to the next line(s).
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _dialogueText;

    private Queue<string> sentences;


    private void Awake()
    {
        sentences = new Queue<string>();
        _dialogueText.text = "";
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _nameText.text = "";
        _nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        SpawnNextLine();
    }

    public void SpawnNextLine()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        _dialogueText.text = "";

        string sentence = sentences.Dequeue();
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
