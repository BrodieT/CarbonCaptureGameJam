using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text captainText = default;
    [SerializeField]
    TMP_Text colonelText = default;
    [SerializeField]
    Image captainBox = default;
    [SerializeField]
    Image colonelBox = default;
    [SerializeField]
    Image captainIcon = default;
    [SerializeField]
    Image colonelIcon = default;
    [SerializeField]
    Vector3 normalScale = Vector3.one;
    [SerializeField]
    Vector3 shrunkenScale = Vector3.one * 0.85f;
    [SerializeField]
    Color normalColorTint = Color.white;
    [SerializeField]
    Color greyedOutColourTint = Color.gray;

    public void DisplayBeat(DialogueBeat beat)
    {
        StopAllCoroutines();
        switch(beat.speaker)
        {
            case DialogueBeat.Speaker.CaptainClimate:
                //Reorder the hierarchy to display the captains box over the other
                colonelBox.transform.SetSiblingIndex(0);
                //Change the colour tints of the two boxes
                StartCoroutine(RecolourBoxes(normalColorTint, greyedOutColourTint, 4, 1.0f));
                //Resize the two boxes
                StartCoroutine(ResizeBoxes(normalScale, shrunkenScale, 1, 1.0f));
                //Display the new contents
                captainText.text = beat.GetDialogueContents();
                break;
            case DialogueBeat.Speaker.ColonelCarbon:
                //Reorder the hierarchy to display the captains box over the other
                captainBox.transform.SetSiblingIndex(0);
                //Change the colour tints of the two boxes
                StartCoroutine(RecolourBoxes(greyedOutColourTint, normalColorTint, 4, 1.0f));
                //Resize the two boxes
                StartCoroutine(ResizeBoxes(shrunkenScale, normalScale, 1, 1.0f));
                //Display the new contents
                colonelText.text = beat.GetDialogueContents();
                break;
        }
       
    }

    IEnumerator ResizeBoxes(Vector3 captainScale, Vector3 colonelScale, float lerpSpeed, float forgivenessAmount)
    {
        bool done = false;
        while (!done)
        {
            colonelBox.rectTransform.localScale = Vector3.Lerp(colonelBox.rectTransform.localScale, colonelScale, Time.deltaTime * lerpSpeed);
            captainBox.rectTransform.localScale = Vector3.Lerp(captainBox.rectTransform.localScale, captainScale, Time.deltaTime * lerpSpeed);
            yield return null;

            if (Vector3.Distance(colonelBox.rectTransform.localScale, colonelScale) < forgivenessAmount
                && Vector3.Distance(captainBox.rectTransform.localScale, captainScale) < forgivenessAmount)
            {
                done = true;
                colonelBox.rectTransform.localScale = colonelScale;
                captainBox.rectTransform.localScale = captainScale;
            }
        }
    }

     IEnumerator RecolourBoxes(Color captainColor, Color colonelColor, float lerpSpeed, float forgivenessAmount)
    {
        bool done = false;
        while (!done)
        {
            colonelBox.color = Color.Lerp(colonelBox.color, colonelColor, Time.deltaTime * lerpSpeed);
            captainBox.color = Color.Lerp(captainBox.color, captainColor, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }
}
