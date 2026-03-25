using UnityEngine;

public class GameOver : MonoBehaviour
{
    /// <summary>
    /// triggers gameover UI 
    /// </summary>
    [SerializeField] GameObject GameoverUI;
    public void Gameover()
    {
        GameoverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
