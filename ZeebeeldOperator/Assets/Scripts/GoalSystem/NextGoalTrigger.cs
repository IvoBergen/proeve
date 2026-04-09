using UnityEngine;
using UnityEngine.Events;

public class NextGoalTrigger : MonoBehaviour, IInterface
{
    [SerializeField] UnityEvent NextGoal;
    private bool hasTriggered;

    public void Interact()
    {
        if (!hasTriggered)
        {
            NextGoal.Invoke(); // This will call GoalManager.NextGoal()
            hasTriggered = true;
        }
    }
}