using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Used to handle the hits of the fruit.
/// </summary>
public class HitCounter : MonoBehaviour
{
    [SerializeField] private int _requiredHits;
    [SerializeField] private TextMeshProUGUI _vegetablesCounter;

    private int _cutSpotHitCounter;

    public static event Action OnRequiredHits;
    public UnityEvent _miniGameEnded;

    public void Setup(int requiredHits)
    {
        _requiredHits = requiredHits;
        _cutSpotHitCounter = 0;

        UpdateUI();
    }

    private void OnEnable()
    {
        HitCutSpots.OnHit += HitCutSpot;
    }

    private void OnDisable()
    {
        HitCutSpots.OnHit -= HitCutSpot;
    }

    private void HitCutSpot()
    {
        _cutSpotHitCounter++;
        CheckCounter();
    }

    private void CheckCounter()
    {
        UpdateUI();

        if (_cutSpotHitCounter < _requiredHits)
            return;

        _cutSpotHitCounter = 0;

        OnRequiredHits?.Invoke();
        _miniGameEnded?.Invoke();
    }

    private void UpdateUI()
    {
        if (_vegetablesCounter != null)
            _vegetablesCounter.text = $"{_cutSpotHitCounter}/{_requiredHits}";
    }
}