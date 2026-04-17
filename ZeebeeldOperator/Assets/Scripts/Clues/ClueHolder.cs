using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// A component that holds a clue and allows the player to interact with it to add the clue to the ClueManager.
    /// </summary>
    public class ClueHolder : MonoBehaviour, IInterface
    {
        private bool _collected = false;
        [SerializeField] private Clue _clue;

        public Clue GetClue()
        {
            return _clue;
        }
        public void Interact()
        {
            AddClueToManager();
        }
        public void AddClueToManager()
        {
            if (_collected == false)
            {
                _collected = true;


                if (ClueManager.Instance != null)
                {
                    ClueManager.Instance.AddClue(_clue);
                    Debug.Log($"Added clue: {_clue.clueName}");
                    // Destroy(this.gameObject);
                }
            }
        }
    }
}