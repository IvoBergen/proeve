using System;
using UnityEngine;

/// <summary>
/// Handles splitting of fruits when cut.
/// </summary>
public class FruitCutHandler : MonoBehaviour
{
    [Serializable]
    public class CutSet
    {
        public GameObject leftPrefab;
        public GameObject rightPrefab;
    }

    [Header("Fruit Prefabs")]
    [SerializeField] private CutSet _kiwiSet;
    [SerializeField] private CutSet _cucumberSet;

    [SerializeField] private float _splitDistance = 5f;

    public static event Action OnAllFruitCut;

    private void OnEnable()
    {
        CutInteractor.OnCut += HandleCut;
    }

    private void OnDisable()
    {
        CutInteractor.OnCut -= HandleCut;
    }

    private void HandleCut(GameObject target)
    {
        if (target == null) return;

        FruitCutLock lockComp = target.GetComponent<FruitCutLock>();
        if (lockComp != null && lockComp.isCut)
            return;

        if (lockComp == null)
            lockComp = target.AddComponent<FruitCutLock>();

        lockComp.isCut = true;

        FruitType fruit = target.GetComponent<FruitType>();
        if (fruit == null) return;

        CutSet set = GetSet(fruit.fruitType);
        if (set == null) return;

        SpawnSplit(set, target.transform);

        Destroy(target);
        CheckRoundComplete();
    }

    private CutSet GetSet(FruitType.Type type)
    {
        switch (type)
        {
            case FruitType.Type.Kiwi:
                return _kiwiSet;

            case FruitType.Type.Cucumber:
                return _cucumberSet;

            default:
                return null;
        }
    }

    private void SpawnSplit(CutSet set, Transform t)
    {
        if (set.leftPrefab == null || set.rightPrefab == null)
        {
            Debug.LogError("CutSet missing prefabs!");
            return;
        }

        Vector3 right = t.right;

        Vector3 leftPos = t.position - right * _splitDistance;
        Vector3 rightPos = t.position + right * _splitDistance;

        Quaternion rot = Quaternion.Euler(0f, 90f, 0f);

        GameObject left = Instantiate(set.leftPrefab, leftPos, rot);
        GameObject rightObj = Instantiate(set.rightPrefab, rightPos, rot);

        float lifeTime = UnityEngine.Random.Range(2f, 5f);

        Destroy(left, lifeTime);
        Destroy(rightObj, lifeTime);
    }

    private void CheckRoundComplete()
    {
        FruitType[] remaining = FindObjectsOfType<FruitType>();

        if (remaining.Length == 0)
        {
            OnAllFruitCut?.Invoke();
        }
    }
}