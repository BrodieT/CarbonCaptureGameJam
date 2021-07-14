using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "Create Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField]
    public List<DialogueBeat> dialogue = new List<DialogueBeat>();
}

[System.Serializable]
public class DialogueBeat
{
[System.Serializable]
public enum Speaker { CaptainClimate = 0, ColonelCarbon = 1 }

    [SerializeField]
    public Speaker speaker = 0;
    [SerializeField, TextArea()]
    public string dialogueContent = "Contents";
    [SerializeField, Min(0)]
    public float beatTime = 5.0f;

    public string GetSpeakerName()
    {
        switch (speaker)
        {
            case Speaker.CaptainClimate:
                return "Captain Climate";
            case Speaker.ColonelCarbon:
                return "Colonel Carbon";
            default:
                return "Speaker Name";
        }
    }

    public string GetDialogueContents()
    {
        return dialogueContent;
    }

    public float GetDialogueTime()
    {
        return beatTime;
    }
}