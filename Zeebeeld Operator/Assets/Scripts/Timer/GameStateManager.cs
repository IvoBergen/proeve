using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    /// <summary>
    /// Handels the winning and losing of the game
    /// </summary>
    [SerializeField] GameObject GameoverUI;
    [SerializeField] GameObject WinUI;
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
}
