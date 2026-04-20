using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mario-style countdown timer with milestone panic animations and safe UI shake.
/// </summary>
public class LevelTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private int _startTime = 300;

    [Header("UI")]
    [SerializeField] private TMP_Text _timerText;

    [Header("Milestone Events")]
    public UnityEvent on4Minutes;
    public UnityEvent on3Minutes;
    public UnityEvent on2Minutes;
    public UnityEvent on1Minute;
    public UnityEvent on30Seconds;
    public UnityEvent onTimeUp;

    [Header("Animation")]
    [SerializeField] private float _scaleMultiplier = 1.1f;
    [SerializeField] private float _animationDuration = 0.15f;
    [SerializeField] private float _shakeIntensity = 5f;
    [SerializeField] private float _milestoneDuration = 0.5f;
    [SerializeField] private float _maxShake = 20f; // clamp maximum shake in units

    private float _currentTime;

    private bool _fired4Min;
    private bool _fired3Min;
    private bool _fired2Min;
    private bool _fired1Min;
    private bool _fired30Sec;
    private bool _firedTimeUp;

    private bool _isPaused;
    private bool _isInMilestoneAnimation;

    private int _lastDisplayedSecond;

    private Vector3 _originalScale;
    private Vector2 _originalPosition; // anchoredPosition
    private Coroutine _animationRoutine;

    private void Start()
    {
        _currentTime = _startTime;

        _originalScale = _timerText.transform.localScale;
        _originalPosition = _timerText.rectTransform.anchoredPosition;

        _lastDisplayedSecond = Mathf.FloorToInt(_currentTime);

        UpdateUI();
    }

    private void Update()
    {
        if (_firedTimeUp || _isPaused) return;

        _currentTime -= Time.deltaTime;

        CheckMilestones();
        UpdateUI();

        if (_currentTime <= 0)
        {
            _currentTime = 0;
            _firedTimeUp = true;

            UpdateUI();
            TriggerMilestone(onTimeUp);
        }
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60);
        int seconds = Mathf.FloorToInt(_currentTime % 60);

        _timerText.text = $"{minutes:00}:{seconds:00}";

        int currentSecond = Mathf.FloorToInt(_currentTime);

        if (!_isInMilestoneAnimation && currentSecond != _lastDisplayedSecond)
        {
            _lastDisplayedSecond = currentSecond;

            if (_animationRoutine != null)
                StopCoroutine(_animationRoutine);

            _animationRoutine = StartCoroutine(AnimateTimer());
        }
    }

    private void CheckMilestones()
    {
        if (!_fired4Min && _currentTime <= 240f)
        {
            _fired4Min = true;
            TriggerMilestone(on4Minutes);
        }

        if (!_fired3Min && _currentTime <= 180f)
        {
            _fired3Min = true;
            TriggerMilestone(on3Minutes);
        }

        if (!_fired2Min && _currentTime <= 120f)
        {
            _fired2Min = true;
            TriggerMilestone(on2Minutes);
        }

        if (!_fired1Min && _currentTime <= 60f)
        {
            _fired1Min = true;
            TriggerMilestone(on1Minute);
        }

        if (!_fired30Sec && _currentTime <= 30f)
        {
            _fired30Sec = true;
            TriggerMilestone(on30Seconds);
        }
    }

    private void TriggerMilestone(UnityEvent milestoneEvent)
    {
        milestoneEvent?.Invoke();

        if (_animationRoutine != null)
            StopCoroutine(_animationRoutine);

        _animationRoutine = StartCoroutine(MilestoneAnimation());
    }

    // ----------------------
    // Normal tick animation
    // ----------------------
    private IEnumerator AnimateTimer()
    {
        float time = 0f;

        while (time < _animationDuration)
        {
            time += Time.deltaTime;
            float t = time / _animationDuration;

            float scale = Mathf.Lerp(_scaleMultiplier, 1f, t);
            _timerText.transform.localScale = _originalScale * scale;

            if (_currentTime <= 30f)
            {
                ShakeText(_shakeIntensity);
            }

            yield return null;
        }

        _timerText.transform.localScale = _originalScale;
        _timerText.rectTransform.anchoredPosition = _originalPosition;
    }

    // ----------------------
    // Milestone override animation
    // ----------------------
    private IEnumerator MilestoneAnimation()
    {
        _isInMilestoneAnimation = true;

        float time = 0f;

        float scaleBoost = _scaleMultiplier * 1.8f;
        float shakeBoost = _shakeIntensity * 3f;

        while (time < _milestoneDuration)
        {
            time += Time.deltaTime;
            float t = time / _milestoneDuration;

            float scale = Mathf.Lerp(scaleBoost, 1f, t);
            _timerText.transform.localScale = _originalScale * scale;

            ShakeText(shakeBoost);

            yield return null;
        }

        _timerText.transform.localScale = _originalScale;
        _timerText.rectTransform.anchoredPosition = _originalPosition;

        _isInMilestoneAnimation = false;
    }

    // ----------------------
    // Shake helper (clamped)
    // ----------------------
    private void ShakeText(float intensity)
    {
        float shakeX = Mathf.Clamp(Random.Range(-1f, 1f) * intensity, -_maxShake, _maxShake);
        float shakeY = Mathf.Clamp(Random.Range(-1f, 1f) * intensity, -_maxShake, _maxShake);

        _timerText.rectTransform.anchoredPosition =
            _originalPosition + new Vector2(shakeX, shakeY);
    }

    // ----------------------
    // Pause Controls
    // ----------------------
    public void PauseTimer() => _isPaused = true;

    public void ResumeTimer() => _isPaused = false;

    public void TogglePause() => _isPaused = !_isPaused;
}