using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsernameInput : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textLetter = default;

    private string currentLetter = default;
    int placement = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetCurrentLetter("A", 0);
    }

    public void SetCurrentLetter(string l, int p)
    {
        currentLetter = l;
        placement = p;

        textLetter.text = currentLetter;
    }

    public string GetCurrentLetter()
    {
        return currentLetter;
    }

    public int GetCurrentPlacement()
    {
        return placement;
    }
}
