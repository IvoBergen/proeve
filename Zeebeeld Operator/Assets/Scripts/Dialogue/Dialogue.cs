
using UnityEngine;

/// <summary>
/// <c>Dialogue</c> a singleton to write the name and lines of dialogue
/// </summary>
[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
