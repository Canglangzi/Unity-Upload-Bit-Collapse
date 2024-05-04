using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EventRelay
{
    private static EventRelay instance;
    private Dictionary<string, List<Action>> eventListeners = new Dictionary<string, List<Action>>();

    private EventRelay() { }

    public static EventRelay Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventRelay();
            }
            return instance;
        }
    }

    public void RegisterListener(string eventName, Action listener)
    {
        if (!eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName] = new List<Action>();
        }
        eventListeners[eventName].Add(listener);
    }

    public void UnregisterListener(string eventName, Action listener)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName].Remove(listener);
            if (eventListeners[eventName].Count == 0)
            {
                eventListeners.Remove(eventName);
            }
        }
    }

    public async Task InvokeEventAsync(string eventName)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            await Task.Run(() =>
            {
                foreach (var listener in eventListeners[eventName])
                {
                    listener?.Invoke();
                }
            });
        }
    }
}