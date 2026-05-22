using System;
using UnityEngine;
/// <summary>
/// Used to go to next round of the chef minigame
/// </summary>
public class RoundTracker : MonoBehaviour
{
    public static RoundTracker Instance;

    private int _activeFruits;

    public static event Action OnRoundComplete;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterFruit()
    {
        _activeFruits++;
    }

    public void UnregisterFruit()
    {
        _activeFruits--;

        if (_activeFruits <= 0)
        {
            _activeFruits = 0;
            OnRoundComplete?.Invoke();
        }
    }

    public void ResetRound()
    {
        _activeFruits = 0;
    }
}