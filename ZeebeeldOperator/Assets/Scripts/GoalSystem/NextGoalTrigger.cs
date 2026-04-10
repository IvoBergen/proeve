using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// is used to trigger the goals and also is able to play a sound
/// </summary>

public class NextGoalTrigger : MonoBehaviour, IInterface
{
    [SerializeField] public UnityEvent NextGoal;
    private bool _hasTriggered;

    public void Interact()
    {
        if (!_hasTriggered)
        {
            NextGoal.Invoke();
            _hasTriggered = true;
        }
    }
}