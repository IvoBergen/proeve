using UnityEngine;
using bnyhtz;

public class GameStateManager : MonoBehaviour
{
    /// <summary>
    /// Handels the state that the game is currently in
    /// </summary>
    [SerializeField] GameObject GameoverUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] PlayerCam playerCam;
    [SerializeField] PlayerMovement playerMovement;
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
        playerCam._movementDisabled = true;
    }
    public void CloseClipboard()
    {
        IsClipboardOpen = false;
        playerCam._movementDisabled = false;
    }
}
