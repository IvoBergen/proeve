using UnityEngine;
/// <summary>
/// stores ship info, names and if they are the enemy
/// </summary>
public class ShipInfo : MonoBehaviour
{
    public enum FlagColour
    {
        Roode,
        Blauwe,
        Gele
    }
    public string currentShipName;
    [SerializeField] private FlagColour _colour;
    [SerializeField] public bool isenemy;
    public void SetName(string name)
    {
        currentShipName = name;
    }
    public FlagColour GetColour()
    {
        return _colour;
    }

    public bool IsEnemy()
    {
        return isenemy;
    }
}