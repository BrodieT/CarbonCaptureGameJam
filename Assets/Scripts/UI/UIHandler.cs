using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance = default;

    public enum MenuNames { MAIN_MENU, GAME_UI, TUTORIAL, SETTINGS, SAVE_SCREEN, LEADERBOARD }

    [System.Serializable]
    public struct UI
    {
        public MenuNames menuName;
        public GameObject obj;
    }

    [SerializeField]
    List<UI> allUI = new List<UI>();

    [SerializeField]
    private bool showMainMenuOnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        if (showMainMenuOnStart)
        {
            SwitchMenu(MenuNames.MAIN_MENU);
        }
    }

    public void SwitchMenu(MenuNames menu)
    {
        foreach(UI u in allUI)
        {
            if(u.menuName == menu)
            {
                u.obj.SetActive(true);
            }
            else
            {
                u.obj.SetActive(false);
            }
        }
    }
}
