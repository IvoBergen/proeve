using TMPro;
using UnityEngine;
/// <summary>
/// handles the timer in the game ui
/// </summary>
public class GameWinUI : MonoBehaviour
{

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