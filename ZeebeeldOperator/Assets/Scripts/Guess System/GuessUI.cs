using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles guessing with 3 selectable options (vertical navigation)
/// </summary>
public class GuessUI : MonoBehaviour
{
    [Header("State")]
    public bool Active;

    [Header("References")]
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private MenuManager _gameUIManager;
    private ShipInfo[] _shipInfo;
    private ShipInfo _selectedShip;
    [SerializeField] private UnityEvent StopGuessing;

    [Header("UI")]
    [SerializeField] private TMP_Text[] shipname;

    [Tooltip("Assign 3 pointer objects here (top, middle, bottom)")]
    [SerializeField] private GameObject[] _pointers;

    private int _currentIndex = 0;

    private void Start()
    {
        _shipInfo = FindObjectsOfType<ShipInfo>();
        UpdatePointer();
    }

    private void Update()
    {
        if (!Active) return;

        _gameUIManager.GuessUIActive = true;
        _gameStateManager.InDialogue();
        _gameStateManager.PauseTimer();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = _pointers.Length - 1;

            UpdatePointer();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _currentIndex++;
            if (_currentIndex >= _pointers.Length)
                _currentIndex = 0;

            UpdatePointer();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Confirm();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopGuess();
            StopGuessing.Invoke();
        }
    }

    public void StartGuess()
    {
        _shipInfo = FindObjectsOfType<ShipInfo>();

        if (_shipInfo.Length < shipname.Length)
        {

            return;
        }

        Active = true;
        _currentIndex = 0;
        for (int i = 0; i < shipname.Length; i++)
        {
            shipname[i].text = _shipInfo[i].currentShipName;
        }

        UpdatePointer();
    }
    public void StopGuess()
    {
        _gameStateManager.exitDialouge();
        _gameStateManager.ResumeTimer();
        _gameUIManager.GuessUIActive = false;

        Active = false;
    }

    private void UpdatePointer()
    {
        for (int i = 0; i < _pointers.Length; i++)
        {
            _pointers[i].SetActive(i == _currentIndex);
        }
    }

    private void Confirm()
    {
        ShipInfo chosenShip = _shipInfo[_currentIndex];

        if (chosenShip.isenemy)
            _gameStateManager.GameWin();
        else
            _gameStateManager.Gameover();

        StopGuess();
    }
}