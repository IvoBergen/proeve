using UnityEngine;

public class CutButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        if (HitCutSpots._currentCutSpot != null)
            HitCutSpots._currentCutSpot.TryHitCutSpot();
    }
}