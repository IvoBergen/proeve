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
    bool _gameHasStarted = false;

    [Header("References")]
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private GameObject _mainMenuHolder;
    [SerializeField] private GameObject _mainMenuSettings;
    [SerializeField] private GameObject _menuHolder;
    [SerializeField] private GameObject _optionsMenuHolder;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private LevelTimer _levelTimer;
    [SerializeField] private bool _inDialogue;
    private void Start()
    {
        _mainMenuHolder.SetActive(true);
        _menuHolder.SetActive(false);
        _optionsMenuHolder.SetActive(false);
        _controlsMenu.SetActive(false);

        _pausemenuActive = false;

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        _gameStateManager.PauseTimer();
        _gameStateManager.InDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_gameHasStarted) return;

            if (GuessUIActive) return;

            if (_inDialogue) return;

            if (_pausemenuActive)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Unpause();
                _pausemenuActive = false;
                return;
            }


            Pause();
            _menuHolder.SetActive(true);
            _pausemenuActive = true;
        }
    }

    /// <summary>
    /// <c>StartGame</c> Switches the game 
    /// </summary>
    public void StartGame()
    {
        _mainMenuHolder.SetActive(false);
        Unpause();
        _gameHasStarted = true;
    }

    /// <summary>
    /// <c>ControlsMenuActivate</c> Opens the controls menu
    /// </summary>
    public void ControlsMenuActivate()
    {
        _controlsMenu.SetActive(true);
        _mainMenuHolder.SetActive(false);
    }

    /// <summary>
    /// <c>ControlsMenuDeactivate</c> Closes the controls menu
    /// </summary>
    public void ControlsMenuDeactivate()
    {
        _controlsMenu.SetActive(false);
        _mainMenuHolder.SetActive(true);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _playerMovement.enabled = true;
        _levelTimer.enabled = true;

    }

    public void ExitGame()
    {

        Application.Quit();
    }


    public void Pause()
    {

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
        _mainMenuHolder.SetActive(true);
        Pause();
    }

    public void OpenOptions()
    {
        _optionsMenuHolder.SetActive(true);
        _menuHolder.SetActive(false);
    }

    public void CloseOptions()
    {
        _optionsMenuHolder.SetActive(false);
        Unpause();
    }

    public void MainMenuSettingsOpen()
    {
        _mainMenuSettings.SetActive(true);
        _mainMenuHolder.SetActive(false);
    }

    public void MainMenuOptionsClose()
    {
        _mainMenuSettings.SetActive(false);
        _mainMenuHolder.SetActive(true);
    }
    public void EnterDialogue()
    {
        _inDialogue = true;
    }
    public void ExitDialogue() { _inDialogue = false; }
}