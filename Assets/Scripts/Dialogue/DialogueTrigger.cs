using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    DialogueData dialogueData = default;

    DialogueUI uiRef = default;

    private bool isDialoguePlaying = false;
    int currentBeat = 0;


    
    public void StartDialogue()
    {
        //Toggle isPlaying flag to prevent multiple dialogues at once
        isDialoguePlaying = true;
        //Reset to the start of the encounter
        currentBeat = 0;

        //Pause the player
        if (PlayerController.instance)
        {
            PlayerController.instance.EnterDialogue(this);
        }
        else
        {
            Debug.LogWarning("Player could not be found when starting dialogue encounter");
        }

        //Show UI and store a reference
        GameObject obj = UIHandler.Instance.GetMenuObject(UIHandler.MenuNames.DIALOGUE);
        if(obj)
        {
            if(obj.TryGetComponent<DialogueUI>(out DialogueUI ui))
            {
                uiRef = ui;
            }
        }
        UIHandler.Instance.SwitchMenu(UIHandler.MenuNames.DIALOGUE);
        //Display the current beat
        DisplayBeat(0);
    }
 

    public void StopDialogue()
    {
        isDialoguePlaying = false;

        if(PlayerController.instance)
        {
            PlayerController.instance.ExitDialogue();
        }
        else
        {
            Debug.LogWarning("Player could not be found when ending dialogue encounter");
        }

        //Hide UI
        UIHandler.Instance.SwitchMenu(UIHandler.MenuNames.GAME_UI);

        if(LevelGenerator.Instance)
            LevelGenerator.Instance.StartLevelCountdown();
        uiRef = null;
    }

    private void DisplayBeat(int beatID)
    {
        if(uiRef)
        {
            uiRef.DisplayBeat(dialogueData.dialogue[beatID]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDialoguePlaying && dialogueData)
        {
            StartDialogue();
        }
    }

    public void NextBeat()
    {
        currentBeat++;

        if(currentBeat >= dialogueData.dialogue.Count)
        {
            StopDialogue();
        }
        else
        {
            DisplayBeat(currentBeat);
        }
    }


    //IEnumerator PlayDialogue()
    //{
    //    StartDialogue();
    //    while (currentBeat < dialogueData.dialogue.Count)
    //    {
    //        DisplayBeat(currentBeat);
    //        yield return new WaitForSeconds(dialogueData.dialogue[currentBeat].GetDialogueTime());
    //        currentBeat++;
    //    }
    //    StopDialogue();
    //}
}
