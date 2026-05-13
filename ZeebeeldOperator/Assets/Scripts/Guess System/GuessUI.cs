using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles guessing with 3 selectable options + confirmation screen
/// </summary>
public class GuessUI : MonoBehaviour
{
    [Header("State")]
    public bool Active;

    private bool _confirming = false;
    private int _confirmIndex = 0; // 0 = Yes, 1 = No

    [Header("References")]
    [SerializeField] private GameStateManager _gameStateManager;
    [SerializeField] private MenuManager _gameUIManager;

    private ShipInfo[] _shipInfo;

    [SerializeField] private UnityEvent StopGuessing;

    [Header("Ship Selection UI")]
    [SerializeField] private TMP_Text[] _shipname;

    [Tooltip("Assign 3 pointer objects here (top, middle, bottom)")]
    [SerializeField] private GameObject[] _pointers;

    [Header("Confirmation UI")]
    [SerializeField] private GameObject _confirmPanel;

    [SerializeField] private TMP_Text _confirmShipText;

    [SerializeField] private GameObject _yesPointer;
    [SerializeField] private GameObject _noPointer;

    private int _currentIndex = 0;

    private void Start()
    {
        _shipInfo = FindObjectsOfType<ShipInfo>();

        _confirmPanel.SetActive(false);

        UpdatePointer();
        UpdateConfirmPointer();
    }

    private void Update()
    {
        if (!Active) return;

        _gameUIManager.GuessUIActive = true;
        _gameStateManager.InDialogue();
        _gameStateManager.PauseTimer();


        if (_confirming)
        {
            HandleConfirmationInput();
            return;
        }


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

        // Open confirmation screen
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            OpenConfirm();
        }
    }

    public void StartGuess()
    {
        _shipInfo = FindObjectsOfType<ShipInfo>();

        if (_shipInfo.Length < _shipname.Length)
            return;

        Active = true;

        _currentIndex = 0;

        for (int i = 0; i < _shipname.Length; i++)
        {
            _shipname[i].text = _shipInfo[i].currentShipName;
        }

        UpdatePointer();
    }

    public void StopGuess()
    {
        _gameStateManager.exitDialouge();
        _gameStateManager.ResumeTimer();

        _gameUIManager.GuessUIActive = false;

        _confirmPanel.SetActive(false);

        Active = false;
        _confirming = false;
    }

    private void UpdatePointer()
    {
        for (int i = 0; i < _pointers.Length; i++)
        {
            _pointers[i].SetActive(i == _currentIndex);
        }
    }



    private void OpenConfirm()
    {
        _confirming = true;
        _confirmIndex = 0;

        ShipInfo chosenShip = _shipInfo[_currentIndex];

        _confirmShipText.text =
            "<color=yellow><b>" +
            chosenShip.currentShipName +
            "</b></color>";

        _confirmPanel.SetActive(true);

        UpdateConfirmPointer();
    }

    private void CloseConfirm()
    {
        _confirming = false;

        _confirmPanel.SetActive(false);
    }

    private void HandleConfirmationInput()
    {
        // Move between YES and NO
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _confirmIndex = 0;
            UpdateConfirmPointer();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _confirmIndex = 1;
            UpdateConfirmPointer();
        }

        // Confirm choice
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // YES
            if (_confirmIndex == 0)
            {
                FinalConfirm();
            }
            // NO
            else
            {
                CloseConfirm();
            }
        }
    }

    private void UpdateConfirmPointer()
    {
        _yesPointer.SetActive(_confirmIndex == 0);
        _noPointer.SetActive(_confirmIndex == 1);
    }

    private void FinalConfirm()
    {
        ShipInfo chosenShip = _shipInfo[_currentIndex];

        if (chosenShip.isenemy)
        {
            _gameStateManager.GameWin();
        }
        else
        {
            _gameStateManager.Gameover();
        }

        StopGuess();
    }
}