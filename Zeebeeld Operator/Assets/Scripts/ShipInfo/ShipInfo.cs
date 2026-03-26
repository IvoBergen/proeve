using UnityEngine;

public class ShipInfo : MonoBehaviour
{
    public string currentShipName;
    [SerializeField] public bool isenemy;

    private void Start()
    {
        currentShipName = ShipManager.Instance.GetRandomName();
    }
}