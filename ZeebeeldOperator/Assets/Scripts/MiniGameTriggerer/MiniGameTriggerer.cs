using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Is used to start up minigames using the interaction system and then closes the mini game again
/// </summary>
public class MiniGameTriggerer : MonoBehaviour, IInterface
{
    [Header("Cameras")]
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject minigameCamera;

    [Header("Settings")]
    [SerializeField] private float closeDelay = 3f;

    [Header("Events")]
    public UnityEvent OnMinigameStarted;
    public UnityEvent OnMinigameEnded;
    [Header("varibles")]
    private bool iscompleted;

    public void Interact()
    {
        if (playerCamera == null || minigameCamera == null)
        {
            Debug.LogWarning("Cameras not assigned on " + gameObject.name);
            return;
        }

        playerCamera.SetActive(false);
        minigameCamera.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        OnMinigameStarted.Invoke();

    }
    public void CloseMinigame()
    {
        StartCoroutine(CloseMinigameRoutine());
    }

    private IEnumerator CloseMinigameRoutine()
    {
        yield return new WaitForSeconds(closeDelay);
        playerCamera.SetActive(true);
        minigameCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnMinigameEnded.Invoke();
        Destroy(gameObject);
    }
}