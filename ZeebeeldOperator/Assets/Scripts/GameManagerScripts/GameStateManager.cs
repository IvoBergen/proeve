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
    [SerializeField] GameWinUI gameWinUI;
    public bool IsClipboardOpen { get; private set; }

    public void EnableGUI()
    {
        gameGUI.SetActive(true);
    }
    public void Gameover()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameoverUI.SetActive(true);
        playerMovement.enabled = false;
        levelTimer.enabled = false;
    }
    public void GameWin()
    {
        gameWinUI.ShowWinScreen();
        WinUI.SetActive(true);
        playerMovement.enabled = false;
        levelTimer.enabled = false;
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
