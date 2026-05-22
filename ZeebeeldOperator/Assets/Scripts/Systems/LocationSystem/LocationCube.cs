using UnityEngine;
using UnityEngine.Serialization;

namespace bnyhtz
{
    /// <summary>
    /// Defines a room transition trigger by storing the old and new room names for the LocationSystem.
    /// </summary>
    public class LocationCube : MonoBehaviour
    {
        [FormerlySerializedAs("_currentRoom")]
        [SerializeField] private string _oldRoom;
        [SerializeField] private string _newRoom;

        public string OldRoom => _oldRoom;
        public string NewRoom => _newRoom;
    }
}
