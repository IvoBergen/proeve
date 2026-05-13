using System;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// <c>HitCutSpots</c> Responsible for making the player be able to cut the vegetable.
/// </summary>
public class HitCutSpots : MonoBehaviour
{
    [SerializeField] private UnityEvent _chopEvent;
    [SerializeField] private int _requiredCutFood = 2;
    [SerializeField] private int _requiredHits;

    public bool canBePressed;

    public static event Action onHit;
    public static HitCutSpots currentCutSpot;

    private void Update()
    {
        HitCutSpot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CutSpot")
        {
            canBePressed = true;
            currentCutSpot = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CutSpot")
        {
            canBePressed = false;
            if (currentCutSpot == this)
                currentCutSpot = null;
        }
    }

    private void HitCutSpot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryHitCutSpot();
        }
    }

    public void TryHitCutSpot()
    {
        if (!canBePressed) return;
        Debug.Log("hit!");

        onHit?.Invoke();

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play("Chop");
        }

        _chopEvent?.Invoke();

        Destroy(gameObject);
    }

}
