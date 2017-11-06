using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMenu : MonoBehaviour
{
    public GameObject menu;

	void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            menu.SetActive(!menu.activeSelf);

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
}
