using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject counterParent;
    public TextMeshProUGUI counterText;

    [Header("Settings")]
    [SerializeField] private int _totalPapers = 4;

    private int _papersFound = 0;
    public bool IsQuestActive { get; private set; }
    private bool _readingNote = false;

    void Start()
    {
        if (counterParent != null) counterParent.SetActive(false);
    }

    public void StartQuest()
    {
        IsQuestActive = true;
        if (counterParent != null) counterParent.SetActive(true);
        UpdateCounterUI();
    }

    void Update()
    {
        // Laat de speler door de tekst klikken, ook al is het briefje al Destroyed
        if (_readingNote)
        {
            DialogueManager dm = FindObjectOfType<DialogueManager>();
            // We gebruiken 'Active' zoals gedefinieerd in jouw DialogueManager
            if (dm != null && dm.Active)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    dm.ShowNextLine();
                }
            }
            else
            {
                _readingNote = false;
            }
        }
    }

    public void OnPaperCollected(Notes note)
    {
        _papersFound++;
        UpdateCounterUI();

        DialogueManager dm = FindObjectOfType<DialogueManager>();
        if (dm == null) return;

        // LOGICA: Als dit het laatste briefje is, pak de clue. Anders de normale tekst.
        string[] sentencesToDisplay = (_papersFound >= _totalPapers)
            ? note.clueSentences
            : note.normalSentences;

        _readingNote = true;
        // We roepen de BeginDialogue aan met de naam en de gekozen zinnen
        dm.BeginDialogue(note.noteName, sentencesToDisplay);

        // Als alles gevonden is, verbergen we de UI na een korte vertraging
        if (_papersFound >= _totalPapers)
        {
            Invoke("HideUI", 3f);
        }

        Destroy(note.gameObject);
    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
            counterText.text = "Papiertjes: " + _papersFound + " / " + _totalPapers;
    }

    private void HideUI()
    {
        if (counterParent != null) counterParent.SetActive(false);
    }
}
