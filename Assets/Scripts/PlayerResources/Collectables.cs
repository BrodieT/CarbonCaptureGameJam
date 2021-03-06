using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectables : MonoBehaviour
{
    public static Collectables Instance = default;

    int totalCO2Collected = 0;

    [SerializeField]
    TextMeshProUGUI collectablesUI = default;

    [SerializeField]
    string textMessage = "";

    [SerializeField]
    int bossDropAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        collectablesUI.text = textMessage + " " + totalCO2Collected.ToString();
    }

    private void Awake()
    {
        EnemyDrops.collectCO2Delegate += CollectedCO2;
    }

    private void OnDestroy()
    {
        EnemyDrops.collectCO2Delegate -= CollectedCO2;
    }

    void CollectedCO2()
    {
        totalCO2Collected++;
        collectablesUI.text = textMessage + " " + totalCO2Collected.ToString();

        LeaderboardHandler.Instance.SetHighScore(totalCO2Collected);
    }

    public void BossDrop()
    {
        totalCO2Collected += bossDropAmount;
        collectablesUI.text = textMessage + " " + totalCO2Collected.ToString();

        LeaderboardHandler.Instance.SetHighScore(totalCO2Collected);
    }

    public void ResetScore()
    {
        totalCO2Collected = 0;
        collectablesUI.text = textMessage + " " + totalCO2Collected.ToString();

        LeaderboardHandler.Instance.SetHighScore(totalCO2Collected);
    }

}
