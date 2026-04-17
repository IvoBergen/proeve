using UnityEngine;

[System.Serializable]
// is used to store the dialogue and repeat dialouge
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] firstTimeSentences;

    [TextArea(3, 10)]
    public string[] repeatSentences;
}