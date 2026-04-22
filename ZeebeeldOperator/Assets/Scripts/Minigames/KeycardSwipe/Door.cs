using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openSpeed = 2.5f;

    private static readonly Vector3 OpenPosition = new Vector3(-11.5714216f, 2.79099989f, -22.2609997f);
    private Coroutine openRoutine;

    public void OpenDoor()
    {
        if (openRoutine != null)
        {
            StopCoroutine(openRoutine);
        }

        openRoutine = StartCoroutine(OpenDoorRoutine());
    }

    private IEnumerator OpenDoorRoutine()
    {
        while (Vector3.Distance(transform.position, OpenPosition) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                OpenPosition,
                openSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = OpenPosition;
        openRoutine = null;
    }
}
