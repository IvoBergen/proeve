using System;

namespace bnyhtz
{
    /// <summary>
    /// Represents a clue in the game, which can be of various categories and have different attributes.
    /// </summary>
    public enum ClueType
    {
        Letter,
        Photo,
        NPC
    }
    [Serializable]
    public class Clue
    {
        public string clueName;
        public ClueType clueType;
        public string clueText;

        public Clue() { }

        public Clue(string clueName, ClueType clueType, string clueText)
        {
            this.clueName = clueName;
            this.clueType = clueType;
            this.clueText = clueText;
        }
    }
}