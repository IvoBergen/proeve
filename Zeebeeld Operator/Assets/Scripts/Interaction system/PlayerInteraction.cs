using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _range = 3f;
    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private LayerMask _interactableLayer;

    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _canvasHolder;

    private IInterface _currentInteractable;

    /// <summary>
    /// Sets out a sphere cast when looking at an interactable show a canvas on screen
    /// </summary>
    void Update()
    {
        DetectInteractable();

        // Show or hide canvas based on current interactable
        _canvasHolder.SetActive(_currentInteractable != null);

        // Interact if pressing E
        if (Input.GetKeyDown(KeyCode.E) && _currentInteractable != null)
        {
            _currentInteractable.Interact();
        }
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        // SphereCast to detect interactable objects
        if (Physics.SphereCast(ray, _radius, out hit, _range, _interactableLayer))
        {
            IInterface interactable = hit.collider.GetComponent<IInterface>();

            if (interactable != null)
            {
                _currentInteractable = interactable;
                return;
            }
        }

        // No interactable found
        _currentInteractable = null;
    }

    private void OnDrawGizmos()
    {
        if (_camera == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_camera.transform.position, _camera.transform.forward * _range);
        Gizmos.DrawWireSphere(
            _camera.transform.position + _camera.transform.forward * _range,
            _radius
        );
    }
}