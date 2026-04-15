using System;
using UnityEngine;
/// <summary>
/// <c>HitCutSpots</c> Responsible for making the player be able to cut the vegetable.
/// </summary>
public class HitCutSpots : MonoBehaviour
{

    [SerializeField] private int _requiredCutFood = 2;
    [SerializeField] private int _requiredHits;

    public bool canBePressed;

    public static event Action onHit;

    private void Update()
    {
        HitCutSpot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CutSpot")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CutSpot")
        {
            canBePressed = false;
        }
    }

    private void HitCutSpot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!canBePressed) return;

            onHit?.Invoke();
            Destroy(gameObject);
        }
    }

}
