using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>MainMenu</c> Controls the behavior of the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private GameObject _mainMenu;

    /// <summary>
    /// <c>StartGame</c> Switches the game 
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("starting game");
    }

    /// <summary>
    /// <c>SettingMenuActivate</c> Opens the settings menu
    /// </summary>
    public void SettingMenuActivate()
    {
        _settingsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    /// <summary>
    /// <c>ControlsMenuActivate</c> Opens the controls menu
    /// </summary>
    public void ControlsMenuActivate()
    {
        _controlsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    /// <summary>
    /// <c>SettingMenuDeactivate</c> Closes the settings menu
    /// </summary>
    public void SettingMenuDeactivate()
    {
        _settingsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    /// <summary>
    /// <c>ControlsMenuDeactivate</c> Closes the controls menu
    /// </summary>
    public void ControlsMenuDeactivate()
    {
        _controlsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    /// <summary>
    /// <c>ExitGame</c> Exits the applications 
    /// </summary>
    public void ExitGame()
    {
        Debug.LogWarning("Quit Game!");
        Application.Quit();
    }
}
