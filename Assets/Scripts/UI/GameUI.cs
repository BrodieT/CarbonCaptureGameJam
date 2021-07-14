using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance = default;

    [SerializeField]
    TextMeshProUGUI countdownText = default;

    [SerializeField]
    TextMeshProUGUI timerText = default;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCountdownText(int i)
    {
        if(i != -1)
        {
            countdownText.text = i.ToString();
            countdownText.gameObject.SetActive(true);
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }
    }

    public void UpdateTimerText(Vector2 t)
    {
        timerText.text = t.x.ToString() + ":" + t.y.ToString();
    }
}
