using UnityEngine;
using UnityEngine.Serialization;

namespace bnyhtz
{
    public class LocationCube : MonoBehaviour
    {
        [FormerlySerializedAs("_currentRoom")]
        [SerializeField] private string _oldRoom;
        [SerializeField] private string _newRoom;

        public string OldRoom => _oldRoom;
        public string NewRoom => _newRoom;
    }
}
