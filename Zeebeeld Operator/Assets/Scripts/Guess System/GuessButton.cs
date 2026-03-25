using UnityEngine;

public class GuessButton : MonoBehaviour, IInterface
{
    [SerializeField] GuessUI GuessUI;

    void ActivateUI()
    {
        GuessUI.ActivateUI();
    }
    public void Interact()
    {
        ActivateUI();
    }
}
