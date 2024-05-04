using System;
using System.Collections.Generic;
using UnityEngine;
public enum MikuEventType
 {
     OnHealthChanged,
    OnPlayerDeath,
    // Add more event types here...
 }

    public class MikuEventAggregator : MonoBehaviour
    {
        private static readonly MikuEventAggregator instance = new MikuEventAggregator();

        public static MikuEventAggregator Instance => instance;

        private readonly Dictionary<MikuEventType, List<Delegate>> _subscribers = new Dictionary<MikuEventType, List<Delegate>>();

        public void Subscribe<TEventData>(MikuEventType eventName, Action<TEventData> subscriber)
        {
            if (!_subscribers.ContainsKey(eventName))
            {
                _subscribers[eventName] = new List<Delegate>();
            }
            _subscribers[eventName].Add(subscriber);
        }

        public void Publish<TEventData>(MikuEventType eventName, TEventData eventData)
        {
            if (_subscribers.ContainsKey(eventName))
            {
                foreach (var subscriber in _subscribers[eventName])
                {
                    if (subscriber is Action<TEventData>)
                    {
                        ((Action<TEventData>)subscriber)(eventData);
                    }
                }
            }
        }

        public void Unsubscribe<TEventData>(MikuEventType eventName, Action<TEventData> subscriber)
        {
            if (_subscribers.ContainsKey(eventName))
            {
                _subscribers[eventName].Remove(subscriber);
                // 替换 Any 方法为检查列表的 Count 属性是否为 0
                if (_subscribers[eventName].Count == 0)
                {
                    _subscribers.Remove(eventName);
                }
            }
        }

      
    }