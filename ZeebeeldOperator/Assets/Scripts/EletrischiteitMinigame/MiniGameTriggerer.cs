using UnityEngine;
using UnityEngine.Events;

public class MiniGameTriggerer : MonoBehaviour, IInterface
{
    [Header("Cameras")]
    [SerializeField] private GameObject playerCamera; // The main FPS/TPS camera
    [SerializeField] private GameObject minigameCamera; // The dedicated puzzle camera

    [Header("Events")]
    public UnityEvent OnMinigameStarted;
    public UnityEvent OnMinigameEnded;

    // This method is required by IInterface
    public void Interact()
    {
        if (playerCamera == null || minigameCamera == null)
        {
            Debug.LogWarning("Cameras not assigned on MiniGameTriggerer!");
            return;
        }

        // 1. Switch Cameras
        playerCamera.SetActive(false);
        minigameCamera.SetActive(true);

        // 2. Enable Mouse Cursor for UI/Puzzle interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 3. Trigger the specific game logic (e.g., EnergyPuzzleModule.StartPuzzle)
        OnMinigameStarted.Invoke();
    }

    public void CloseMinigame()
    {
        // 1. Revert Cameras
        playerCamera.SetActive(true);
        minigameCamera.SetActive(false);

        // 2. Re-lock Mouse for Gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 3. Signal completion to external systems (e.g., Radar script)
        OnMinigameEnded.Invoke();
    }
}