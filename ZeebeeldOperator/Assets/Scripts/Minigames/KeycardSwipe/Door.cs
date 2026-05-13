using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the door movement for the keycard minigame and opens it smoothly when the swipe is completed.
/// </summary>
public class Door : MonoBehaviour
{
    [SerializeField] private float _openSpeed = 2.5f;
    [SerializeField] private Transform _openPosition;

    private Coroutine _openRoutine;

    public void OpenDoor()
    {
        if (_openRoutine != null)
        {
            StopCoroutine(_openRoutine);
        }

        _openRoutine = StartCoroutine(OpenDoorRoutine());
    }

    private IEnumerator OpenDoorRoutine()
    {
        while (Vector3.Distance(transform.position, _openPosition.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _openPosition.position,
                _openSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = _openPosition.position;
        _openRoutine = null;
    }
}
