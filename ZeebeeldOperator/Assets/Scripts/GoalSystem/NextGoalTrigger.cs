using UnityEngine;
using UnityEngine.Events;

public class NextGoalTrigger : MonoBehaviour, IInterface
{
    [SerializeField] UnityEvent NextGoal;
    private bool Hastriggerd;
    public void Interact()
    {
        if (Hastriggerd == false)
        {
            NextGoal.Invoke();
            Hastriggerd = true;
        }
    }
}
