using UnityEngine;
using UnityEngine.SceneManagement;

namespace bnyhtz
{
    /// <summary>
    /// Handles resetting the current game scene back to its initial state.
    /// </summary>
    public class GameReset : MonoBehaviour
    {
        public static void ResetGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
