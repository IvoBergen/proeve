using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// <c>HitCounter</c> CHecks how many spots and vegetables have been cut.
/// </summary>
public class HitCounter : MonoBehaviour
{

    [SerializeField] private int _requiredCutFood = 2;
    [SerializeField] private int _requiredHits;
    [SerializeField] private GenerateCutSpots _cutSpots;
    [SerializeField] private TextMeshProUGUI _vegetablesCounter;

    private int _finishedCuttingCounter;
    private int _cutSpotHitCounter;
    private int _finishedVegetables;


    public static event Action onRequiredHits;
    public static event Action onFinishedCutting;

    public UnityEvent miniGameEnded;

    private void OnEnable() => HitCutSpots.onHit += HitCutSpot;
    private void OnDisable() => HitCutSpots.onHit -= HitCutSpot;

    private void Awake()
    {
        _cutSpots = GetComponent<GenerateCutSpots>();
        _finishedVegetables = _requiredCutFood;
    }
    private void Start()
    {
        _requiredHits = _cutSpots.cubeCount;
        _vegetablesCounter.text = _finishedVegetables.ToString();
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
        _vegetablesCounter.text = _finishedVegetables.ToString();


        if (_cutSpotHitCounter < _requiredHits) return;

        _cutSpotHitCounter = 0;
        _finishedCuttingCounter++;
        _finishedVegetables--;

        if (_finishedCuttingCounter == _requiredCutFood)
        {
            miniGameEnded.Invoke();
            return;
        }

        onRequiredHits?.Invoke();

    }

    public void EndMinigame()
    {
        this.enabled = false;
        _cutSpots.enabled = false;
        _vegetablesCounter.enabled = false;
        _cutSpots.uiHolder.SetActive(false);

    }
}
