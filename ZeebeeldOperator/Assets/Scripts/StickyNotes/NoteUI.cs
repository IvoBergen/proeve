using TMPro;
using UnityEngine;

public class NoteUI : MonoBehaviour
{
    [Header("Counter UI")]
    public TextMeshProUGUI counterText;
    private int _papersFound = 0;
    private int _totalPapers = 4;

    [Header("Clue UI")]
    public GameObject cluePanel; // Het achtergrondje voor de tekst
    public TextMeshProUGUI clueText;

    void Start()
    {
        UpdateCounterUI();
        cluePanel.SetActive(false); // Verberg de clue tekst aan het begin
    }

    public void OnPaperCollected(bool isHint, string content)
    {
        _papersFound++;
        UpdateCounterUI();

        if (isHint)
        {
            ShowClue(content);
        }
    }

    private void UpdateCounterUI()
    {
        counterText.text = "Papiertjes: " + _papersFound + " / " + _totalPapers;
    }

    private void ShowClue(string content)
    {
        clueText.text = content;
        cluePanel.SetActive(true);

        // Optioneel: Laat de tekst na 10 seconden weer verdwijnen
        // Invoke("HideClue", 10f); 
    }

    public void HideClue()
    {
        cluePanel.SetActive(false);
    }
}
