using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// A component that holds a clue and allows the player to interact with it to add the clue to the ClueManager.
    /// </summary>
    public class ClueHolder : MonoBehaviour, IInterface
    {

        [SerializeField] private Clue clue;

        public Clue GetClue()
        {
            return clue;
        }
        public void Interact()
        {
            AddClueToManager();
        }
        public void AddClueToManager()
        {
            if (ClueManager.Instance != null)
            {
                ClueManager.Instance.AddClue(clue);
                Debug.Log($"Added clue: {clue.clueName}");
                // Destroy(this.gameObject);
            }
        }
    }
}