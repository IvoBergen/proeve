using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the UI of the game such as the pause and options menu.
/// </summary>
public class GameUIManager : MonoBehaviour
{
    [Header("Variables")]
    public bool GuessUIActive;

    [Header("References")]
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private GameObject _PauseMenuHolder;
    [SerializeField] private GameObject _optionsMenuHolder;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GuessUIActive)
                return;

            if (_optionsMenuHolder.activeSelf)
            {
                CloseOptions();
                return;
            }

            // Toggle pause menu
            if (_PauseMenuHolder.activeSelf)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Pause()
    {
        _PauseMenuHolder.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _gameStateManager.PauseTimer();
    }

    public void Unpause()
    {
        _PauseMenuHolder.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _gameStateManager.ResumeTimer();
        _gameStateManager.exitDialouge();

        _optionsMenuHolder.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenOptions()
    {
        _optionsMenuHolder.SetActive(true);
        _PauseMenuHolder.SetActive(false);
    }

    public void CloseOptions()
    {
        _optionsMenuHolder.SetActive(false);
        _PauseMenuHolder.SetActive(true);
    }
}