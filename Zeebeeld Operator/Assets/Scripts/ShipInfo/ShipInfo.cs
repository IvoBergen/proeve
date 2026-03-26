using UnityEngine;
/// <summary>
/// stores ship info, names and if they are the enemy
/// </summary>
public class ShipInfo : MonoBehaviour
{
    public string currentShipName;
    [SerializeField] public bool isenemy;

    private void Start()
    {
        currentShipName = ShipManager.Instance.GetRandomName();
    }
}