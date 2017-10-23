using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> events;

    private static EventManager instance;

    public static EventManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<EventManager>();

            if (instance == null)
            {
                Debug.LogError("Event Manager was not found.");
            }
            else
            {
                instance.Init();
            }
        }

        return instance;
    }

    void Init()
    {
        if (events == null)
        {
            events = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListen(string name, UnityAction action)
    {
        UnityEvent @event;
        if (GetInstance().events.TryGetValue(name, out @event))
        {
            @event.AddListener(action);
        }
        else
        {
            @event = new UnityEvent();
            @event.AddListener(action);
            GetInstance().events.Add(name, @event);
        }
    }

    public static void StopListen(string name, UnityAction action)
    {
        if (instance == null)
            return;

        UnityEvent @event;
        if (GetInstance().events.TryGetValue(name, out @event))
        {
            @event.RemoveListener(action);
        }
    }

    public static void TriggerEvent(string name)
    {
        UnityEvent @event;
        if (GetInstance().events.TryGetValue(name, out @event))
        {
            @event.Invoke();
        }
    }
}
