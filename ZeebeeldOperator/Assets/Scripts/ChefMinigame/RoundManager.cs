using TMPro;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Used to handel the rounds of the chef minigame
/// </summary>
public class RoundManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] private int _maxRounds = 2;

    private int _currentRound = 0;
    private bool _roundActive;

    public UnityEvent gameOverEvent;

    [Header("References")]
    [SerializeField] private GenerateCutSpots _spawner;

    [Header("UI")]
    [SerializeField] private GameObject _uiHolder;
    [SerializeField] private TextMeshProUGUI _roundText;

    private void Update()
    {
        if (!_roundActive) return;

        CheckIfRoundFinished();
    }

    private void StartRound()
    {
        if (_uiHolder != null)
            _uiHolder.SetActive(true);

        _currentRound++;

        if (_currentRound > _maxRounds)
        {
            GameOver();
            return;
        }

        _roundActive = true;

        UpdateUI();

        if (_spawner != null)
        {
            _spawner.SpawnCubesForRound(_currentRound);
        }
    }

    private void CheckIfRoundFinished()
    {
        FruitType[] remaining = FindObjectsOfType<FruitType>();

        if (remaining.Length == 0)
        {
            _roundActive = false;
            StartRound();
        }
    }

    private void UpdateUI()
    {
        if (_roundText != null)
            _roundText.text = $"Round {_currentRound}/{_maxRounds}";
    }

    private void GameOver()
    {
        if (_roundText != null)
            _roundText.text = "Complete!";

        if (_uiHolder != null)
            _uiHolder.SetActive(false);

        _roundActive = false;
        gameOverEvent?.Invoke();
    }
}