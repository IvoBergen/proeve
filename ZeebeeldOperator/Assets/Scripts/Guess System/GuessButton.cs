using UnityEngine;

public class GuessButton : MonoBehaviour, IInterface
{
    /// <summary>
    /// Stores the ship number
    /// </summary>
    [SerializeField] private GuessUI guessUI;
    [SerializeField] private int shipnumber;

    public void Interact()
    {
        guessUI.ActivateUI(shipnumber);
    }
}