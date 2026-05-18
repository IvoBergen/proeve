using System;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// handels the cutting of the fruit
/// </summary>
public class CutInteractor : MonoBehaviour
{
    public static event Action<GameObject> OnCut;

    public UnityEvent _chopped;

    private GameObject _currentTarget;
    private bool _canCut;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canCut && _currentTarget != null)
        {
            OnCut?.Invoke(_currentTarget);
            _chopped?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FruitType>() != null)
        {
            _canCut = true;
            _currentTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _currentTarget)
        {
            _canCut = false;
            _currentTarget = null;
        }
    }
}