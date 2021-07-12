using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardHandler : MonoBehaviour
{
    public static LeaderboardHandler Instance = default;

    private string Letters = "ABCDEFGHIJKLMNOPQRSTUVWX";

    private string userName = default;
    private int score = 0;
    [SerializeField]
    List<UsernameInput> usernameInputs = new List<UsernameInput>();

    [SerializeField]
    private SaveData saveData = default;

    [SerializeField]
    private TextMeshProUGUI informationText = default;

    [SerializeField]
    List<SaveData.SaveDataStructure> allData = new List<SaveData.SaveDataStructure>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void SetHighScore(int s)
    {
        score = s;
    }

    public void UpLetter(UsernameInput u)
    {
        int current = u.GetCurrentPlacement();

        current++;

        if(current > Letters.Length - 1)
        {
            current = 0;
        }

        u.SetCurrentLetter(Letters[current].ToString(), current);
    }

    public void DownLetter(UsernameInput u)
    {
        int current = u.GetCurrentPlacement();

        current--;
        if (current < 0)
        {
            current = Letters.Length - 1;
        }

        u.SetCurrentLetter(Letters[current].ToString(), current);
    }

    public void SaveScore()
    {
        userName = "";

        foreach(UsernameInput u in usernameInputs)
        {
            userName += u.GetCurrentLetter();
        }

        if (!saveData.CheckSaveData(userName))
        {
            SaveData.SaveDataStructure newSaveData = new SaveData.SaveDataStructure();
            newSaveData.playerName = userName;
            newSaveData.playerScore = score;

            saveData.saveData.Add(newSaveData);
        }
        else
        {
            informationText.text = "Username already exists, try again!";
            Invoke("ClearInformationText", 3);
        }
    }

    void ClearInformationText()
    {
        informationText.text = "";
    }

    public void GetAllHighscores()
    {
        allData.Clear();
        saveData.SortList();
        allData.AddRange(saveData.GetAllSaveData());
    }

}
