using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
    private MonoBehaviour targetObject;
    private string targetFunction;

    public Image image;
    public Text text;

    public bool hideMenuOnSelect;

    public void SetOnSelectParameters(MonoBehaviour targetObject, string targetFunction)
    {
        this.targetObject = targetObject;
        this.targetFunction = targetFunction;
    }

    public bool OnSelect()
    {
        targetObject.SendMessage(targetFunction);
        return hideMenuOnSelect;
    }
}
