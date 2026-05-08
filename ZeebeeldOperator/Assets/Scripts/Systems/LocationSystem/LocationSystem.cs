using TMPro;
using UnityEngine;

namespace bnyhtz
{
    public class LocationSystem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _startRoom;
        [SerializeField] private string _currentRoom;

        private LocationCube _lastChangedLocationCube;
        private int _lastChangedFrame = -1;

        public string CurrentRoom => _currentRoom;

        private void Start()
        {
            if (!string.IsNullOrEmpty(_startRoom))
            {
                _currentRoom = _startRoom;
            }

            UpdateRoomText();
        }

        private void OnTriggerEnter(Collider other)
        {
            LocationCube locationCube = other.GetComponent<LocationCube>();

            if (locationCube == null)
            {
                locationCube = other.GetComponentInParent<LocationCube>();
            }

            if (locationCube == null) return;
            if (_lastChangedLocationCube == locationCube && _lastChangedFrame == Time.frameCount) return;

            if (ChangeLocation(locationCube))
            {
                _lastChangedLocationCube = locationCube;
                _lastChangedFrame = Time.frameCount;
            }
        }

        private bool ChangeLocation(LocationCube locationCube)
        {
            if (_currentRoom == locationCube.NewRoom)
            {
                _currentRoom = locationCube.OldRoom;
                UpdateRoomText();
                return true;
            }

            if (_currentRoom == locationCube.OldRoom)
            {
                _currentRoom = locationCube.NewRoom;
                UpdateRoomText();
                return true;
            }

            return false;
        }

        private void UpdateRoomText()
        {
            if (_text == null) return;

            _text.text = _currentRoom;

            Debug.Log("Updated Current Room: " + _currentRoom);
        }
    }
}
