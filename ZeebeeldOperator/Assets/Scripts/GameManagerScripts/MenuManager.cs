using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// handels the ui of the game such as the pause and options menu.
/// </summary>
public class MenuManager : MonoBehaviour
{

    [Header("Variables")]
    public bool GuessUIActive;
    private bool _pausemenuActive;

    [Header("References")]
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private GameObject _menuHolder;
    [SerializeField] private GameObject _optionsMenuHolder;
    [SerializeField] private GameObject _controlsMenu;

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

    /// <summary>
    /// <c>StartGame</c> Switches the game 
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("starting game");
    }

    /// <summary>
    /// <c>ControlsMenuActivate</c> Opens the controls menu
    /// </summary>
    public void ControlsMenuActivate()
    {
        _controlsMenu.SetActive(true);
        _menuHolder.SetActive(false);
    }

    /// <summary>
    /// <c>ControlsMenuDeactivate</c> Closes the controls menu
    /// </summary>
    public void ControlsMenuDeactivate()
    {
        _controlsMenu.SetActive(false);
        _menuHolder.SetActive(true);
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
        _menuHolder.SetActive(true);

        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _gameStateManager.PauseTimer();
        _gameStateManager.InDialogue();
    }

    public void Unpause()
    {
        _pausemenuActive = false;
        _menuHolder.SetActive(false);

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
        _menuHolder.SetActive(false);
    }

    public void CloseOptions()
    {
        _optionsMenuHolder.SetActive(false);
        _menuHolder.SetActive(true);
    }
}