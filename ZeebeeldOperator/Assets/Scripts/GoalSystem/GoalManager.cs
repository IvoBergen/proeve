// GoalManager.cs
using bnyhtz;
using System.Collections;
using TMPro;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] TMP_Text currentGoal;
    [SerializeField] TMP_Text ClipBoardGoal;
    [SerializeField] string[] goals;

    [Header("Animation Settings")]
    [SerializeField] float scaleMultiplier = 2f;
    [SerializeField] float animationDuration = 0.2f;

    [Header("Fade Settings")]
    [SerializeField] float fadeDelay = 3f;
    [SerializeField] float fadeDuration = 1f;

    [Header("Clue Settings")]
    [SerializeField] string clueName = "CurrentGoal";

    private int currentIndex = 0;
    private Vector3 originalScale;
    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        originalScale = currentGoal.transform.localScale;

        // Ensure CanvasGroup exists
        canvasGroup = currentGoal.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = currentGoal.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 1f;
    }

    void Start()
    {
        UpdateGoal(); // Sets initial goal and clue
        TriggerFadeSequence();
    }

    public void NextGoal()
    {
        if (currentIndex < goals.Length - 1)
        {
            currentIndex++;
            UpdateGoal();

            // Reset scale and alpha
            currentGoal.transform.localScale = originalScale;
            canvasGroup.alpha = 1f;

            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            StartCoroutine(PlayPopAnimation());
            TriggerFadeSequence();
        }
    }

    private void UpdateGoal()
    {
        string goalText = goals[currentIndex];

        // Update main goal
        if (currentGoal != null)
            currentGoal.text = goalText;

        // Mirror to clipboard goal
        if (ClipBoardGoal != null)
            ClipBoardGoal.text = goalText;

        // Update clue in ClueManager
        if (ClueManager.Instance != null)
        {
            Clue existingClue = ClueManager.Instance.GetClueByName(clueName);
            if (existingClue != null)
                existingClue.clueText = goalText;
            else
                ClueManager.Instance.AddClue(new Clue { clueName = clueName, clueText = goalText });
        }
    }

    private void TriggerFadeSequence()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAfterDelay());
    }

    IEnumerator PlayPopAnimation()
    {
        Vector3 targetScale = originalScale * scaleMultiplier;
        float time = 0f;

        // Scale up
        while (time < animationDuration)
        {
            currentGoal.transform.localScale = Vector3.Lerp(originalScale, targetScale, time / animationDuration);
            time += Time.deltaTime;
            yield return null;
        }

        currentGoal.transform.localScale = targetScale;

        // Scale down
        time = 0f;
        while (time < animationDuration)
        {
            currentGoal.transform.localScale = Vector3.Lerp(targetScale, originalScale, time / animationDuration);
            time += Time.deltaTime;
            yield return null;
        }

        currentGoal.transform.localScale = originalScale;
    }

    IEnumerator FadeAfterDelay()
    {
        yield return new WaitForSeconds(fadeDelay);

        float time = 0f;
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}