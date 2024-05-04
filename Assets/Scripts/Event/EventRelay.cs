using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EventRelay<T>
{
    private static EventRelay<T> instance;
    private Dictionary<CLZ_EventType, List<Action<T>>> eventListeners = new Dictionary<CLZ_EventType, List<Action<T>>>();
    private Dictionary<CLZ_EventType, Queue<Action<T>>> listenerPool = new Dictionary<CLZ_EventType, Queue<Action<T>>>();
    private Dictionary<CLZ_EventType, int> maxPoolSize = new Dictionary<CLZ_EventType, int>(); // 每种事件类型的对象池最大容量

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

    public void RegisterListener(CLZ_EventType eventType, Action<T> listener)
    {
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType] = new List<Action<T>>();
            listenerPool[eventType] = new Queue<Action<T>>();
            maxPoolSize[eventType] = 10; // 默认对象池最大容量为10
        }

        // Reuse listener from pool if available
        if (listenerPool[eventType].Count > 0)
        {
            var pooledListener = listenerPool[eventType].Dequeue();
            eventListeners[eventType].Add(pooledListener);
        }
        else
        {
            eventListeners[eventType].Add(listener);
        }
    }

    public void UnregisterListener(CLZ_EventType eventType, Action<T> listener)
    {
        if (eventListeners.ContainsKey(eventType))
        {
            eventListeners[eventType].Remove(listener);

            // Add listener back to pool
            if (listenerPool[eventType].Count < maxPoolSize[eventType])
            {
                listenerPool[eventType].Enqueue(listener);
            }
        }
    }

    public async Task InvokeEventAsync(CLZ_EventType eventType, T param)
    {
        if (eventListeners.ContainsKey(eventType))
        {
            try
            {
                await Task.Run(() =>
                {
                    Queue<Action<T>> listeners = new Queue<Action<T>>(eventListeners[eventType]);

                    while (listeners.Count > 0)
                    {
                        var listener = listeners.Dequeue();
                        listener?.Invoke(param);
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred while invoking event {eventType}: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"No listeners found for event {eventType}");
        }
    }

}