using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Assigns ships fixed names automatically based on hierarchy order
/// </summary>
public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance;

    [SerializeField] private string[] allNames;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        AssignNamesToShips();
    }

    private void AssignNamesToShips()
    {
        ShipInfo[] ships = FindObjectsOfType<ShipInfo>();
        List<ShipInfo> sortedShips = ships
            .OrderBy(ship => ship.transform.GetSiblingIndex())
            .ToList();

        for (int i = 0; i < sortedShips.Count; i++)
        {
            string name = (i < allNames.Length)
                ? allNames[i]
                : "Unnamed Ship";

            sortedShips[i].SetName(name);
        }
    }
}