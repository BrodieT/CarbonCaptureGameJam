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

    public string GetDialogueContents()
    {
        return dialogueContent;
    }
}