using TMPro;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Handels guessing and checks if the ship is the enemyship 
/// </summary>
public class GuessUI : MonoBehaviour
{
    [Header("Variables")]
    public bool Active;

    [Header("Events")]
    [SerializeField] private UnityEvent onGuessUIOpen;
    [SerializeField] private UnityEvent onGuessUIClose;
    [SerializeField] private UnityEvent onCorrectGuess;
    [SerializeField] private UnityEvent onWrongGuess;
    [SerializeField] private UnityEvent onCancelGuess;
    [Header("refrences")]
    [SerializeField] private GameObject _guessUI;
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
        onGuessUIOpen?.Invoke();
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.A)))
        {
            _currentIndex = 0;
            UpdatePointer();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D)))
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

        _guessUI.SetActive(true);

        onGuessUIOpen?.Invoke();

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
                onCorrectGuess?.Invoke();
            }
            else
            {
                onWrongGuess?.Invoke();
            }

            CloseUI();
        }
        else
            CloseUI();
    }
    private void CloseUI()
    {
        _guessUI.SetActive(false);

        onGuessUIClose?.Invoke();

        Active = false;
    }
}