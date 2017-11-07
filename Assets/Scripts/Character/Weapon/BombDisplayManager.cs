using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombDisplayManager : MonoBehaviour, IPickable
{
    public Text granadeText;

    void Awake()
    {
        UpdateText();
    }

    public void Add()
    {
        PickUpManager.Add(Constants.BombPickUpName, 1);
        UpdateText();
    }

    public void UpdateText()
    {
        granadeText.text = PickUpManager.GetCount(Constants.BombPickUpName).ToString();
    }
}
