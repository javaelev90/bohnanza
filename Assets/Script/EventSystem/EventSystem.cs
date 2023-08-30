using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : SingletonMonobehaviour<EventSystem>
{
    public delegate void EventListener(Event e);
    private Dictionary<Type, HashSet<EventListener>> eventListeners = new Dictionary<Type, HashSet<EventListener>>();

    public void RegisterListener<T>(Action<T> listener) where T : Event
    {
        Type eventType = typeof(T);
        EventListener wrapper = (ev) => { listener((T)ev); };
        if (!eventListeners.ContainsKey(eventType))
        {
            eventListeners.Add(eventType, new HashSet<EventListener>());
        }
        eventListeners[eventType].Add(wrapper);
    }

    public void UnregisterListener<T>(Action<T> listener) where T : Event
    {
        Type eventType = typeof(T);
        EventListener wrapper = (ev) => { listener((T)ev); };
        if (!eventListeners.ContainsKey(eventType) || !eventListeners[eventType].Contains(wrapper))
        {
            return;
        }
        eventListeners[eventType].Remove(wrapper);
    }

    public void FireEvent(Event e)
    {
        Type eventType = e.GetType();

        if (eventListeners.ContainsKey(eventType) == false)
        {
            return;
        }

        foreach (EventListener eventListener in eventListeners[eventType])
        {
            eventListener(e);
        }
    }
}
