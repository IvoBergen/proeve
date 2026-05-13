using System.Collections;
using UnityEngine;


/// <summary>
/// Script is used to tranfser the camera to the top of the sonar. with a small delay
/// </summary>

public class SonarCamTriggerer : MonoBehaviour, IInterface
{

    [SerializeField] private bool _triggered;
    [SerializeField] private MiniGameTriggerer _triggerer;
    [SerializeField] private float _delay = 1f;

    public void Interact()
    {
        if (_triggered)
            return;

        _triggered = true;
        _triggerer.Interact();

        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(_delay);

        _triggerer.CloseMinigame();
        _triggered = false;
    }
}