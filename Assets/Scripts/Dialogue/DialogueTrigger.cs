using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    DialogueData dialogueData = default;
    [SerializeField]
    GameObject dialogueUIPrefab = default;

    DialogueUI uiRef = default;

    PlayerController playerRef = default;

    private bool isDialoguePlaying = false;
    int currentBeat = 0;

    public void StartDialogue()
    {
        //Toggle isPlaying flag to prevent multiple dialogues at once
        isDialoguePlaying = true;
        //Reset to the start of the encounter
        currentBeat = 0;

        //Pause the player
        if (playerRef)
        {
            playerRef.EnterDialogue(this);
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

        if(playerRef)
        {
            playerRef.ExitDialogue();
        }
        //Hide UI
        UIHandler.Instance.SwitchMenu(UIHandler.MenuNames.GAME_UI);
        uiRef = null;
    }

    private void DisplayBeat(int beatID)
    {
        if(uiRef)
        {
            uiRef.DisplayBeat(dialogueData.dialogue[beatID]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isDialoguePlaying && dialogueData)
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                playerRef = player;
                StartDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRef = null;
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


    IEnumerator PlayDialogue()
    {
        StartDialogue();
        while (currentBeat < dialogueData.dialogue.Count)
        {
            DisplayBeat(currentBeat);
            yield return new WaitForSeconds(dialogueData.dialogue[currentBeat].GetDialogueTime());
            currentBeat++;
        }
        StopDialogue();
    }
}
