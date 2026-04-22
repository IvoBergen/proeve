using UnityEngine;

public class SonarManager : MonoBehaviour
{
    [SerializeField] private GameObject _sonarObject;
    [SerializeField] private Material _sonarMaterial;

    public void TurnOnSonar()
    {
        if (_sonarObject != null && _sonarMaterial != null)
        {
            Renderer rend = _sonarObject.GetComponent<Renderer>();

            if (rend != null)
            {
                rend.material = _sonarMaterial;
                Debug.Log("Sonar material applied!");
            }
            else
            {
                Debug.LogError("No Renderer found on the Sonar Object.");
            }
        }
        else
        {
            Debug.LogWarning("Sonar Object or Material is missing in the Inspector.");
        }
    }
}