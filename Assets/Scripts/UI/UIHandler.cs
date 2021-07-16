using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance = default;

    public enum MenuNames { MAIN_MENU, GAME_UI, TUTORIAL, SETTINGS, SAVE_SCREEN, LEADERBOARD, WORLD_MAP, DIALOGUE }

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


    [SerializeField]
    public GameObject playerHealthbar = default;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        if (showMainMenuOnStart)
        {
            SwitchMenu(MenuNames.MAIN_MENU);
        }
        else
        {
            HideAll();
        }
    }

    public GameObject GetMenuObject(MenuNames menu)
    {
        int index = -1;
        index = allUI.FindIndex(x => x.menuName == menu);

        if(index >= 0)
        {
            return allUI[index].obj;
        }

        return null;
    }

    public void HideAll()
    {
        foreach (UI u in allUI)
        {
            u.obj.SetActive(false);
        }
    }


    public void SwitchMenu(MenuNames menu)
    {
        foreach(UI u in allUI)
        {
            if(u.menuName == menu)
            {
                if(u.menuName != MenuNames.GAME_UI)
                {
                    PlayerController.instance.PausePlayer();
                    CameraController.instance.PauseCamera();
                }

                u.obj.SetActive(true);

            }
            else
            {
                u.obj.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        AudioHandler.Instance.PlaySound(AudioBank.Audio.GAME_MUSIC);
    }

    public void RestartFullGame()
    {
        SceneManager.LoadScene(0);
    }
}
