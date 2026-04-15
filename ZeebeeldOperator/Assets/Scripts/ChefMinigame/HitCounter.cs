using System;
using UnityEngine;

public class HitCounter : MonoBehaviour
{

    [SerializeField] private int _requiredCutFood = 2;
    [SerializeField] private int _requiredHits;
    [SerializeField] private GenerateCutSpots _cutSpots;

    private int _finishedCuttingCounter;
    private int _cutSpotHitCounter;


    public static event Action onRequiredHits;
    public static event Action onFinishedCutting;

    private void OnEnable() => HitCutSpots.onHit += HitCutSpot;
    private void OnDisable() => HitCutSpots.onHit -= HitCutSpot;

    private void Awake()
    {
        _cutSpots = GetComponent<GenerateCutSpots>();
    }
    private void Start()
    {
        _requiredHits = _cutSpots.cubeCount;
    }

    private void Update()
    {
        CounterCheck();
    }

    private void HitCutSpot()
    {
        _cutSpotHitCounter++;
    }

    private void CounterCheck()
    {
        if (_finishedCuttingCounter >= _requiredCutFood)
        {
            this.enabled = false;
            _cutSpots.enabled = false;
            return;
        }

        if (_cutSpotHitCounter >= _requiredHits)
        {
            onRequiredHits?.Invoke();
            _cutSpotHitCounter = 0;
            _finishedCuttingCounter++;
        }
    }
}
