using System;
using System.Collections.Generic;

[UnityEngine.Scripting.Preserve]
public class EventManager
{
    private Dictionary<string, Action<object>> eventDictionary;

    private static EventManager instance;

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;
        }
    }

    private EventManager()
    {
        eventDictionary = new Dictionary<string, Action<object>>();
    }

    public void AddListener(string eventName, Action<object> listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] = null;

        eventDictionary[eventName] += listener;
    }

    public void RemoveListener(string eventName, Action<object> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] -= listener;
    }

    public void TriggerEvent(string eventName, object eventData = null)
    {
        if (eventDictionary.ContainsKey(eventName) && eventDictionary[eventName] != null)
            eventDictionary[eventName](eventData);
    }
}
