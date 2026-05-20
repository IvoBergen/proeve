using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Used to generate fruit in a line
/// </summary>
public class GenerateCutSpots : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] _cubePrefabs;

    [SerializeField] private float _minSpacing = 1.5f;

    private Vector3 _cubeHeight = new Vector3(0, 0.1f, 0);
    private List<Vector3> _spawnedPositions = new List<Vector3>();

    public int cubeCount = 3;

    public void SpawnCubesForRound(int round)
    {
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
                float t = Random.Range(0f, 1f);
                spawnPos = Vector3.Lerp(_startPoint.position, _endPoint.position, t) + _cubeHeight;
                maxAttempts--;
            }
            while (!IsValid(spawnPos) && maxAttempts > 0);

            if (maxAttempts > 0)
            {
                _spawnedPositions.Add(spawnPos);

                GameObject prefab = _cubePrefabs[Random.Range(0, _cubePrefabs.Length)];

                GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
                obj.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            }
        }
    }

    private bool IsValid(Vector3 pos)
    {
        foreach (var p in _spawnedPositions)
        {
            if (Vector3.Distance(pos, p) < _minSpacing)
                return false;
        }
        return true;
    }
}