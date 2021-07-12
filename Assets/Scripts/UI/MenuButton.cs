using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private UIHandler.MenuNames menu;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Switch);
    }

    public void Switch()
    {
        UIHandler.Instance.SwitchMenu(menu);
    }
}
