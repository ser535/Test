using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventManager : MonoBehaviour
{
    public static EventManager S;

    Dictionary<string, Action<string>> eventHandlers = new Dictionary<string, Action<string>>();

    private void Awake()
    {
        if (S == null) S = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public Action Subscribe<T>(Action<T> fn, bool fireOnce = false)
    {
        var eventName = typeof(T).ToString();

        void Handler(string json)
        {
            var data = JsonUtility.FromJson<T>(json);
            fn(data);
            if (fireOnce)
                Unsub();
        }

        void Unsub()
        {
            eventHandlers[eventName] -= Handler;
        }

        lock (this)
        {
            if (!eventHandlers.ContainsKey(eventName))
                eventHandlers[eventName] = Handler;
            else
                eventHandlers[eventName] += Handler;
        }
        
        return Unsub;
    }

    public void Fire<T>(T dataObject)
    {
        var json = JsonUtility.ToJson(dataObject);
        var eventName = typeof(T).ToString();
        eventHandlers.TryGetValue(eventName, out var handler);
        handler?.Invoke(json);
    }
}
