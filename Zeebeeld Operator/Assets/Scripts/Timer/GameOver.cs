using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject GameoverUI;
    public void Gameover()
    {
        GameoverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
