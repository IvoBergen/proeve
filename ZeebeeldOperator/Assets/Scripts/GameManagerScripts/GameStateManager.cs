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
    [SerializeField] LevelTimer levelTimer;
    [SerializeField] Clipboard clipboard;
    [SerializeField] GameObject gameGUI;
    public bool IsClipboardOpen { get; private set; }

    public void EnableGUI()
    {
        gameGUI.SetActive(true);
    }

    public void DisableGUI()
    {
        gameGUI.SetActive(false);
    }
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
    }
    public void exitDialouge()
    {
        playerCam._movementDisabled = false;
        playerMovement.movementdisabled = false;
    }
    public void PauseTimer()
    {
        levelTimer.PauseTimer();
    }
    public void ResumeTimer()
    {
        levelTimer.ResumeTimer();
    }

    public void OpenClipboard()
    {
        IsClipboardOpen = true;
    }
    public void CloseClipboard()
    {
        IsClipboardOpen = false;
    }
}
