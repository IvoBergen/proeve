using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>GenerateCutSpots</c> Generates the spots in the bar for the player to cut.
/// </summary>
public class GenerateCutSpots : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private ArrowMover _arrowMover;
    [SerializeField] private float _minSpacing = 1.5f;

    private Vector3 _cubeHeight = new Vector3(0, 0.1f, 0);
    private List<Vector3> _spawnedPositions = new List<Vector3>();

    public GameObject uiHolder;
    public int cubeCount = 3;

    private void OnEnable() => HitCounter.onRequiredHits += SpawnNewCubes;
    private void OnDisable() => HitCounter.onRequiredHits -= SpawnNewCubes;

    private void Awake()
    {
        _arrowMover = GetComponent<ArrowMover>();
        uiHolder.SetActive(false);
    }
    public void SpawnNewCubes()
    {
        if (!enabled) return;

        _arrowMover.enabled = true;
        uiHolder.SetActive(true);

        SpawnCubes();
    }

    private void SpawnCubes()
    {
        _spawnedPositions.Clear();
        for (int i = 0; i < cubeCount; i++)
        {
            Vector3 spawnPos;
            int maxAttempts = 10;

            do
            {
                float randomT = Random.Range(0f, 1f);
                spawnPos = Vector3.Lerp(_startPoint.position, _endPoint.position, randomT) + _cubeHeight;
                maxAttempts--;
            }
            while (!IsPositionValid(spawnPos) && maxAttempts > 0);

            if (maxAttempts > 0)
            {
                _spawnedPositions.Add(spawnPos);
                Instantiate(_cubePrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"Could not place cube {i + 1}, bar might be too full!");
            }
        }
    }

    private bool IsPositionValid(Vector3 pos)
    {
        foreach (Vector3 existing in _spawnedPositions)
        {
            if (Vector3.Distance(pos, existing) < _minSpacing)
                return false;
        }
        return true;
    }
}
