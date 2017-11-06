using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    private int selectedMenuIndex;
    private MenuOption[] menuOptions;

    public Sprite selectImage;
    public Sprite unselectImage;

    public Transform parentObject;
    public MenuOption menuOptionTemplate;

    public MenuOptionParameters[] parameters;

    void Awake()
    {
        menuOptions = new MenuOption[parameters.Length];
        for (int i = 0; i < parameters.Length; i++)
        {
            var menuOption = Instantiate(menuOptionTemplate, parentObject);
            menuOption.image.sprite = parameters[i].selected ? selectImage : unselectImage;
            menuOption.text.text = parameters[i].optionText;
            menuOption.hideMenuOnSelect = parameters[i].hideMenuOnSelect;
            menuOption.SetOnSelectParameters(parameters[i].targetObject, parameters[i].targetFunction);

            if (parameters[i].selected)
                selectedMenuIndex = i;

            menuOptions[i] = menuOption;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            var hideMenu = menuOptions[selectedMenuIndex].OnSelect();
            if (hideMenu)
            {
                this.gameObject.SetActive(false);
            }
        }

        if (Input.GetButtonDown("Vertical"))
        {
            var vertical = Input.GetAxisRaw("Vertical");
            if (vertical > Constants.axisThreshold)
            {
                SelectNextOption(-1);
            }
            else if (vertical < -Constants.axisThreshold)
            {
                SelectNextOption(1);
            }
        }
    }

    private void SelectNextOption(int incrementor)
    {
        var nextIndex = selectedMenuIndex + incrementor;
        if (nextIndex >= 0 && nextIndex < menuOptions.Length)
        {
            menuOptions[selectedMenuIndex].image.sprite = unselectImage;
            menuOptions[nextIndex].image.sprite = selectImage;
            selectedMenuIndex = nextIndex;
        }
    }
}

[Serializable]
public class MenuOptionParameters
{
    public string optionText;
    public bool selected;

    public MonoBehaviour targetObject;
    public string targetFunction;

    public bool hideMenuOnSelect;
}
