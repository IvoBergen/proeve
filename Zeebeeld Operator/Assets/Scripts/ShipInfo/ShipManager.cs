using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance;

    [SerializeField] private string[] allNames;

    private List<string> availableNames;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            availableNames = new List<string>(allNames);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string GetRandomName()
    {
        if (availableNames.Count == 0)
        {
            Debug.LogWarning("No names left!");
            return "Unnamed Ship";
        }

        int index = Random.Range(0, availableNames.Count);
        string name = availableNames[index];

        availableNames.RemoveAt(index);
        return name;
    }
}