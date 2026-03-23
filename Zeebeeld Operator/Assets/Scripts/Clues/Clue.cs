using System;
using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// Represents a clue in the game, which can be of various categories and have different attributes.
    /// </summary>
    public enum ClueCategory
    {
        Letter,
        Photo,
        NPC
    } 
    public enum ClueFlagColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
    public enum ShipDirection
    {
        North,
        South,
        East,
        West
    }
    [Serializable]
    public class Clue
    {
        public string clueName; 
        public ClueCategory category;
        public ClueFlagColor flagColor;
        public ShipDirection direction;
        public string description;

        public Clue() {}

        public Clue(string clueName, ClueCategory category, ClueFlagColor flagColor, ShipDirection direction, string description)
        {
            this.clueName = clueName;
            this.category = category;
            this.flagColor = flagColor;
            this.direction = direction;
            this.description = description;
        }
    }
}