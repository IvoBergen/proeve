using UnityEngine;

public class CutButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        if (HitCutSpots.currentCutSpot != null)
            HitCutSpots.currentCutSpot.TryHitCutSpot();
    }
}
