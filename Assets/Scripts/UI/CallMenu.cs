using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMenu : MonoBehaviour
{
    public GameObject menu;

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            if (menu.activeSelf == false)
            {
                Show(true);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Show(!menu.activeSelf);
        }
	}

    private void Show(bool show)
    {
        menu.SetActive(show);

        if (menu.activeSelf)
        {
            GameManager.GetInstance().StopPlaying();
        }
        else
        {
            GameManager.GetInstance().StartPlaying();
        }
    }
}
