using UnityEngine;

public class GuessUI : MonoBehaviour
{
    [Header("Variables")]
    public bool Active;

    [Header("refrences")]
    [SerializeField] private GameObject _guessUI;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private PlayerCam _playerCam;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private LevelTimer _timer;

    [Header("UI")]
    [SerializeField] private GameObject _yesTarget;
    [SerializeField] private GameObject _noTarget;

    private int _currentIndex = 0; // 0 = Yes, 1 = No

    private void OnEnable()
    {
        UpdatePointer();
    }

    private void Update()
    {
        if (!Active) return;

        _guessUI.SetActive(true);
        _playerCam._movementDisabled = true;
        _playerMovement.movementdisabled = true;
        _timer.PauseTimer();

        // LEFT
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentIndex = 0;
            UpdatePointer();
        }

        // RIGHT
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentIndex = 1;
            UpdatePointer();
        }

        // CONFIRM
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Confirm();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _guessUI.SetActive(false);
            _playerCam._movementDisabled = false;
            _playerMovement.movementdisabled = false;
            Active = false;
            _timer.ResumeTimer();
        }
    }

    public void ActivateUI()
    {
        Active = true;
        _currentIndex = 0;
        UpdatePointer();
    }

    private void UpdatePointer()
    {
        _yesTarget.SetActive(_currentIndex == 0);
        _noTarget.SetActive(_currentIndex == 1);
    }
    // Change this when ship info becomes a thing
    private void Confirm()
    {
        if (_currentIndex == 0)
        {
            _gameStateManager.GameWin();
        }
        else
        {
            _guessUI.SetActive(false);
            _playerCam._movementDisabled = false;
            _playerMovement.movementdisabled = false;
            Active = false;
            _timer.ResumeTimer();
        }

        Active = false;
        _guessUI.SetActive(false);
    }
}