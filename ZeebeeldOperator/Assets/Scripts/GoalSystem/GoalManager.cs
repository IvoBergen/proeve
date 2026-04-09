using System.Collections;
using TMPro;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField] TMP_Text currentGoal;
    [SerializeField] string[] goals;

    [Header("Animation Settings")]
    [SerializeField] float scaleMultiplier = 2f;
    [SerializeField] float animationDuration = 0.2f;

    private int currentIndex = 0;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = currentGoal.transform.localScale;
        UpdateGoal();
    }

    public void NextGoal()
    {
        if (currentIndex < goals.Length - 1)
        {
            currentIndex++;
            UpdateGoal();
            StartCoroutine(PlayPopAnimation());
        }
    }

    void UpdateGoal()
    {
        currentGoal.text = goals[currentIndex];
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
}