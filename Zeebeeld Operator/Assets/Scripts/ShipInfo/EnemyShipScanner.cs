using bnyhtz;
using System;
using System.Collections;
using UnityEngine;

public class EnemyShipScanner : MonoBehaviour, IInterface
{
    public void Interact()
    {
        StartCoroutine(ScanAndSendClueWithDelay());
    }

    private IEnumerator ScanAndSendClueWithDelay()
    {
        Debug.Log("triggered");
        yield return new WaitForSeconds(5f);

        if (ClueManager.Instance == null)
        {
            Debug.LogError("ClueManager not found in scene!");
            yield break;
        }

        ShipInfo[] ships = FindObjectsOfType<ShipInfo>();

        ShipInfo enemyShip = Array.Find(ships, ship => ship.IsEnemy());

        if (enemyShip == null)
        {
            Debug.LogError("No enemy ship found!");
            yield break;
        }

        // --- COLOUR CLUE ---
        ShipInfo.FlagColour colour = enemyShip.GetColour();
        string colourClueText = $"het vijandelijk schip heeft een {colour} flag.";

        Clue colourClue = new Clue(
            "Enemy Flag Colour",
            ClueType.Letter,
            colourClueText
        );

        ClueManager.Instance.AddClue(colourClue);
        yield return new WaitForSeconds(2f);
        // --- NAME CLUE ---
        string shipName = enemyShip.currentShipName;
        string nameClueText = $"het vijandelijke schip heet '{shipName}'.";

        Clue nameClue = new Clue(
            "Enemy Ship Name",
            ClueType.Letter,
            nameClueText
        );

        ClueManager.Instance.AddClue(nameClue);
    }
}