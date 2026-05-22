using UnityEngine;
/// <summary>
/// Used for the interactAble for the Binoculairs
/// </summary>
public class VerreKijkerInterActable : MonoBehaviour, IInterface
{
    [SerializeField] private GameObject _SchipUI;
    [SerializeField] private GameObject _Player;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Interact()
    {
        _SchipUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _Player.SetActive(false);
    }

    public void ExitBincolaurs()
    {
        _SchipUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _Player?.SetActive(true);
    }
}
