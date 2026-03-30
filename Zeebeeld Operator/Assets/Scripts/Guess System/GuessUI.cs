using TMPro;
using UnityEngine;
/// <summary>
/// Handels guessing and checks if the ship is the enemyship 
/// </summary>
public class GuessUI : MonoBehaviour
{
    [Header("Variables")]
    public bool Active;

    [Header("refrences")]
    [SerializeField] private GameObject _guessUI;
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] ShipInfo[] _shipInfo;
    private ShipInfo _selectedShip;
    [Header("UI")]
    [SerializeField] TMP_Text shipname;
    [SerializeField] private GameObject _yesTarget;
    [SerializeField] private GameObject _noTarget;

    private int _currentIndex = 0;

    private void OnEnable()
    {
        UpdatePointer();
    }
    private void Start()
    {
        _shipInfo = FindObjectsOfType<ShipInfo>();
    }

    private void Update()
    {
        if (!Active) return;

        _guessUI.SetActive(true);
        _gameStateManager.InDialogue();
        _gameStateManager.PauseTimer();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentIndex = 0;
            UpdatePointer();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentIndex = 1;
            UpdatePointer();
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Confirm();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUI();
        }
    }

    public void ActivateUI(int shipIndex)
    {
        if (shipIndex < 0 || shipIndex >= _shipInfo.Length)
        {
            Debug.LogWarning("Invalid ship index!");
            return;
        }

        _selectedShip = _shipInfo[shipIndex];

        Active = true;
        _currentIndex = 0;

        shipname.text = _selectedShip.currentShipName;

        UpdatePointer();
    }

    private void UpdatePointer()
    {
        _yesTarget.SetActive(_currentIndex == 0);
        _noTarget.SetActive(_currentIndex == 1);
    }
    private void Confirm()
    {
        if (_currentIndex == 0)
        {
            if (_selectedShip.isenemy)
            {
                _gameStateManager.GameWin();
            }
            else
            {
                _gameStateManager.Gameover();
            }
        }
        else
        {
            CloseUI();
            return;
        }

        CloseUI();
    }
    private void CloseUI()
    {
        _guessUI.SetActive(false);
        _gameStateManager.exitDialouge();
        Active = false;
        _gameStateManager.ResumeTimer();
    }
}