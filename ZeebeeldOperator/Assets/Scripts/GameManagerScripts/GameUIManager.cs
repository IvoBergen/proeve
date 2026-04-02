using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// handels the ui of the game such as the pause and options menu.
/// </summary>
public class GameUIManager : MonoBehaviour
{

    [Header("Variables")]
    public bool GuessUIActive;
    private bool _pausemenuActive;

    [Header("References")]
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private GameObject _PauseMenuHolder;
    [SerializeField] private GameObject _optionsMenuHolder;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GuessUIActive)
            {
                return;
            }

            if (_pausemenuActive)
            {
                Unpause();
                _pausemenuActive = false;
                return;
            }

            Pause();
            _pausemenuActive = true;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {

        EditorApplication.isPlaying = false;

        Application.Quit();
    }

    public void Pause()
    {
        _PauseMenuHolder.SetActive(true);

        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _gameStateManager.PauseTimer();
        _gameStateManager.InDialogue();
    }

    public void Unpause()
    {
        _pausemenuActive = false;
        _PauseMenuHolder.SetActive(false);

        // Lock the cursor back to the center for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _gameStateManager.ResumeTimer();
        _gameStateManager.exitDialouge();
        _optionsMenuHolder.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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