using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



public class EventRelay<T>
{
    private static EventRelay<T> instance;
    private Dictionary<EventType, List<Action<T>>> eventListeners = new Dictionary<EventType, List<Action<T>>>();

    private EventRelay() { }

    public static EventRelay<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventRelay<T>();
            }
            return instance;
        }
    }

    public void RegisterListener(EventType eventType, Action<T> listener)
    {
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<Action<T>>();
        }
        eventListeners[eventType].Add(listener);
    }

    public void UnregisterListener(EventType eventType, Action<T> listener)
    {
        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType].Remove(listener);
            if (eventListeners[eventType].Count == 0)
            {
                eventListeners.Remove(eventType);
            }
        }
    }

    public async Task InvokeEventAsync(EventType eventType, T param)
    {
        if (eventListeners.ContainsKey(eventType))
        {
            await Task.Run(() =>
            {
                foreach (var listener in eventListeners[eventType])
                {
                    listener?.Invoke(param);
                }
            });
        }
    }
}