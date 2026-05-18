using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Responsible for making the player able to cut the vegetable.
/// </summary>
public class HitCutSpots : MonoBehaviour
{
    [SerializeField] private UnityEvent _chopEvent;
    [SerializeField] private int _requiredHits;

    [Header("Split Prefabs")]
    [SerializeField] private GameObject _leftPrefab;
    [SerializeField] private GameObject _rightPrefab;
    [SerializeField] private float _splitDistance = 5f;

    private bool _canBePressed;
    private GameObject _currentSpot;

    public static event Action OnHit;
    public static HitCutSpots _currentCutSpot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryHitCutSpot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CutSpot"))
        {
            _canBePressed = true;
            _currentCutSpot = this;
            _currentSpot = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CutSpot"))
        {
            _canBePressed = false;

            if (_currentCutSpot == this)
                _currentCutSpot = null;

            _currentSpot = null;
        }
    }

    public void TryHitCutSpot()
    {
        if (!_canBePressed || _currentSpot == null) return;

        OnHit?.Invoke();

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play("Chop");
        }

        _chopEvent?.Invoke();

        SpawnSplitPieces(_currentSpot.transform);
        Destroy(_currentSpot);
    }

    private void SpawnSplitPieces(Transform spot)
    {
        Vector3 right = spot.right;

        Vector3 leftPos = spot.position - right * _splitDistance;
        Vector3 rightPos = spot.position + right * _splitDistance;

        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject left = Instantiate(_leftPrefab, leftPos, rotation);
        GameObject rightObj = Instantiate(_rightPrefab, rightPos, rotation);

        float destroyTime = UnityEngine.Random.Range(2f, 5f);

        Destroy(left, destroyTime);
        Destroy(rightObj, destroyTime);
    }
}