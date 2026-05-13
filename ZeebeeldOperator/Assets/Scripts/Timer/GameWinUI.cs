using TMPro;
using UnityEngine;

public class GameWinUI : MonoBehaviour
{
    /// <summary>
    /// handles the timer in the game ui
    /// </summary>
    [Header("References")]
    [SerializeField] private LevelTimer _levelTimer;
    [SerializeField] private TMP_Text _finalTimeText;

    [Header("Win Screen")]
    [SerializeField] private GameObject _winScreen;

    private void Start()
    {
        if (_winScreen != null)
        {
            _winScreen.SetActive(false);
        }
    }

    public void ShowWinScreen()
    {

        _levelTimer.SaveFinalTime();

        _finalTimeText.text =
            "Gehaald in" + _levelTimer.GetSavedCompletionTime();


        if (_winScreen != null)
        {
            _winScreen.SetActive(true);
        }


    }
}