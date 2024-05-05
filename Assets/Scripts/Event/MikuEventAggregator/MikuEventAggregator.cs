using System;
using System.Collections.Generic;
using UnityEngine;
public enum MikuEventType
 {
     OnHealthChanged,
     OnPlayerDeath,
 
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
               
                if (_subscribers[eventName].Count == 0)
                {
                    _subscribers.Remove(eventName);
                }
            }
        }

      
    }