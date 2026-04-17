using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles goal display, animation, and fading.
/// </summary>
public class GoalManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentGoalText;
    [SerializeField] private TMP_Text _clipboardGoalText;
    [SerializeField] private string[] _goals;

    [Header("Animation Settings")]
    [SerializeField] private float _scaleMultiplier = 2f;
    [SerializeField] private float _animationDuration = 0.2f;

    [Header("Fade Settings")]
    [SerializeField] private float _fadeDelay = 3f;
    [SerializeField] private float _fadeDuration = 1f;

    private int _currentGoalIndex = -1;
    private Vector3 _originalScale;
    private CanvasGroup _canvasGroup;
    private Coroutine _fadeCoroutine;

    private void Awake()
    {
        _originalScale = _currentGoalText.transform.localScale;

        _canvasGroup = _currentGoalText.GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = _currentGoalText.gameObject.AddComponent<CanvasGroup>();

        _canvasGroup.alpha = 1f;
    }

    private void Start()
    {
        SetGoal(0);
    }

    public void SetGoal(int index)
    {
        if (index < 0 || index >= _goals.Length)
        {
            Debug.LogWarning($"Goal index {index} is out of range.");
            return;
        }

        _currentGoalIndex = index;
        UpdateGoal();

        _currentGoalText.transform.localScale = _originalScale;
        _canvasGroup.alpha = 1f;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        StartCoroutine(PlayPopAnimation());
        TriggerFadeSequence();
    }

    private void UpdateGoal()
    {
        string goalText = _goals[_currentGoalIndex];

        _currentGoalText.text = goalText;

        if (_clipboardGoalText != null)
            _clipboardGoalText.text = goalText;
    }

    private void TriggerFadeSequence()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeAfterDelay());
    }

    private IEnumerator PlayPopAnimation()
    {
        Vector3 targetScale = _originalScale * _scaleMultiplier;

        float time = 0f;
        while (time < _animationDuration)
        {
            _currentGoalText.transform.localScale =
                Vector3.Lerp(_originalScale, targetScale, time / _animationDuration);

            time += Time.deltaTime;
            yield return null;
        }

        _currentGoalText.transform.localScale = targetScale;

        time = 0f;
        while (time < _animationDuration)
        {
            _currentGoalText.transform.localScale =
                Vector3.Lerp(targetScale, _originalScale, time / _animationDuration);

            time += Time.deltaTime;
            yield return null;
        }

        _currentGoalText.transform.localScale = _originalScale;
    }

    private IEnumerator FadeAfterDelay()
    {
        yield return new WaitForSeconds(_fadeDelay);

        float time = 0f;
        while (time < _fadeDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / _fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        _canvasGroup.alpha = 0f;
    }
}