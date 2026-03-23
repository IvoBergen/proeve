using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mario-style countdown timer using TextMeshPro.
/// Counts down from a start time, updates UI, and fires milestone events.
/// </summary>
public class LevelTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float _startTime = 120f;

    [Header("UI")]
    [SerializeField] private TMP_Text _timerText;

    [Header("Milestone Events")]
    public UnityEvent on90Seconds;
    public UnityEvent on60Seconds;
    public UnityEvent on30Seconds;
    public UnityEvent onTimeUp;

    private float _currentTime;

    private bool _fired90;
    private bool _fired60;
    private bool _fired30;
    private bool _firedTimeUp;

    private bool _isPaused;

    private void Start()
    {
        _currentTime = _startTime;
        UpdateUI();
    }

    private void Update()
    {
        // Stop everything if time is up or paused
        if (_firedTimeUp || _isPaused) return;

        _currentTime -= Time.deltaTime;

        CheckMilestones();
        UpdateUI();

        if (_currentTime <= 0)
        {
            _currentTime = 0;
            _firedTimeUp = true;
            UpdateUI();
            onTimeUp.Invoke();
        }
    }

    private void CheckMilestones()
    {
        if (!_fired90 && _currentTime <= 90)
        {
            _fired90 = true;
            on90Seconds.Invoke();
        }

        if (!_fired60 && _currentTime <= 60)
        {
            _fired60 = true;
            on60Seconds.Invoke();
        }

        if (!_fired30 && _currentTime <= 30)
        {
            _fired30 = true;
            on30Seconds.Invoke();
        }
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60);
        int seconds = Mathf.FloorToInt(_currentTime % 60);
        _timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void PauseTimer()
    {
        _isPaused = true;
    }


    public void ResumeTimer()
    {
        _isPaused = false;
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
    }
}