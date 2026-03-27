using UnityEngine;
/// <summary>
/// stores ship info, names and if they are the enemy
/// </summary>
/// 
public class ShipInfo : MonoBehaviour
{
    public enum FlagColour
    {
        Roode,
        Blauwe,
        Gele
    }
    public string currentShipName;
    [SerializeField] private FlagColour colour;
    [SerializeField] public bool isenemy;
    public void SetName(string name)
    {
        currentShipName = name;
    }
    public FlagColour GetColour()
    {
        return colour;
    }

    public bool IsEnemy()
    {
        return isenemy;
    }
}