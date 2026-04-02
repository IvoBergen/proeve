using UnityEngine;

/// <summary>
/// Central state coordinator for game flow, dialogue, timer, and clipboard open state.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject GameoverUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] PlayerCam playerCam;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameUIManager gameUIManager;
    [SerializeField] LevelTimer levelTimer;
    [SerializeField] Clipboard clipboard;
    public bool IsClipboardOpen { get; private set; }

    public void Gameover()
    {
        GameoverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void GameWin()
    {
        WinUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void InDialogue()
    {
        playerCam._movementDisabled = true;
        playerMovement.movementdisabled = true;
        gameUIManager.GuessUIActive = true;
    }
    public void exitDialouge()
    {
        playerCam._movementDisabled = false;
        playerMovement.movementdisabled = false;
        gameUIManager.GuessUIActive = false;
        Debug.Log("called");
    }
    public void PauseTimer()
    {
        playerCam._movementDisabled = true;
        playerMovement.movementdisabled = true;
        levelTimer.PauseTimer();
    }
    public void ResumeTimer()
    {
        levelTimer.ResumeTimer();
        playerCam._movementDisabled = false;
        playerMovement.movementdisabled = false;
    }

    public void OpenClipboard()
    {
        IsClipboardOpen = true;
        playerCam._movementDisabled = true;
    }
    public void CloseClipboard()
    {
        IsClipboardOpen = false;
        playerCam._movementDisabled = false;
    }
}
