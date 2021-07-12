using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsernameButton : MonoBehaviour
{
    [SerializeField]
    private bool up = false;

    [SerializeField]
    private UsernameInput usernameInput = default;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnPressButton);
    }

    void OnPressButton()
    {
        if (up)
        {
            LeaderboardHandler.Instance.UpLetter(usernameInput);
        }
        else
        {
            LeaderboardHandler.Instance.DownLetter(usernameInput);
        }
    }
}
