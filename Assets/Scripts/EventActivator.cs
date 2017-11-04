using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActivator : MonoBehaviour
{
    public string eventName;
    public GameObject objectToActivate;

    void OnEnable()
    {
        EventManager.StartListen(eventName, ActivateObject);
    }

    void OnDisable()
    {
        EventManager.StopListen(eventName, ActivateObject);
    }

    private void ActivateObject()
    {
        objectToActivate.SetActive(true);
    }
}
