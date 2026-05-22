using UnityEngine;
/// <summary>
/// Used to store the type of fruit
/// </summary>
public class FruitType : MonoBehaviour
{
    public enum Type
    {
        Kiwi,
        Cucumber
    }

    public Type fruitType;

    private void Start()
    {
        RoundTracker.Instance.RegisterFruit();
    }
}