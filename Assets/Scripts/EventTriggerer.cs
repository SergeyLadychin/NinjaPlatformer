using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerer : MonoBehaviour
{
    private bool triggered;

    public string triggerTargetTag;
    public string eventName;
    public bool triggerOnce;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((triggered && !triggerOnce) || (!triggered && triggerOnce) || (!triggered && !triggerOnce)) && other.CompareTag(triggerTargetTag))
        {
            EventManager.TriggerEvent(eventName);
            triggered = true;
        }
    }
}
