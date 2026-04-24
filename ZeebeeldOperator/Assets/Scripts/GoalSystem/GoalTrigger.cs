using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Triggers a specific goal index on interaction.
/// </summary>
public class GoalTrigger : MonoBehaviour, IInterface
{
    [SerializeField] private GoalManager _goalManager;
    [SerializeField] private int _goalIndex;

    [SerializeField] private UnityEvent OnTriggered;

    private bool _hasTriggered;

    public void Interact()
    {
        if (_hasTriggered) return;

        _goalManager.SetGoal(_goalIndex);
        OnTriggered?.Invoke();

        _hasTriggered = true;
    }
}