using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    public static HighscoreTable Instance = default;

    [SerializeField]
    private TextMeshProUGUI leaderboardName = default;

    [SerializeField]
    private Transform leaderboardParent = default;

    List<SaveData.SaveDataStructure> data = new List<SaveData.SaveDataStructure>();

    [SerializeField]
    [Range(1, 15)]
    private int highScoreLimit = 5;

    int current = 0;
    // Start is called before the first frame update

    private void Start()
    {
        Instance = this;
    }

    public void PopulateTable()
    {
        data.Clear();
        data.AddRange(LeaderboardHandler.Instance.GetAllHighscores());


        foreach (SaveData.SaveDataStructure save in data)
        {
            TextMeshProUGUI text = Instantiate(leaderboardName);
            text.text = save.playerName + " ---------- " + save.playerScore.ToString();
            text.transform.SetParent(leaderboardParent);

            current++;

            if (current == highScoreLimit)
            {
                break;
            }
        }
    }
}
