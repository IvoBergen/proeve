using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Is used to start up minigames using the interaction system and then closes the mini game again
/// </summary>
public class MiniGameTriggerer : MonoBehaviour, IInterface
{
    [Header("Cameras")]
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private GameObject _minigameCamera;

    [Header("Settings")]
    [SerializeField] private float _closeDelay = 3f;

    [Header("Events")]
    public UnityEvent OnMinigameStarted;
    public UnityEvent OnMinigameEnded;
    [Header("varibles")]
    private bool _iscompleted;

    public void Interact()
    {
        if (_playerCamera == null || _minigameCamera == null)
        {
            Debug.LogWarning("Cameras not assigned on " + gameObject.name);
            return;
        }

        _playerCamera.SetActive(false);
        _minigameCamera.SetActive(true);

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
        yield return new WaitForSeconds(_closeDelay);
        _playerCamera.SetActive(true);
        _minigameCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnMinigameEnded.Invoke();
    }
}