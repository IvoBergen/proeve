using System.Collections.Generic;
using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// Singleton class that manages the player's collected clues. Provides methods to add clues and retrieve them.
    /// </summary>
    public class ClueManager : MonoBehaviour
    {
        public static ClueManager Instance;

        private List<Clue> clues = new List<Clue>();

        private void Awake()
        {
            Instance = this;
        }

        public void AddClue(Clue clue)
        {
            clues.Add(clue);
        }

        public List<Clue> GetAllClues()
        {
            return clues;
        }

        public Clue GetClueByName(string clueName)
        {
            for (int i = 0; i < clues.Count; i++)
            {
                if (clues[i].clueName == clueName)
                {
                    return clues[i];
                }
            }

            return null;
        }
    }
}