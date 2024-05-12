using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SingletonNetworkBehaviour<T> : NetworkBehaviour where T : Component
{
    public static T Instance { get; private set; }

    /// <summary>
    /// Singleton pattern, makes sure to only keep one instance of a specific component
    /// </summary>
    protected virtual void OnEnable()
    {
        if (Instance is object && this is T && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this as T;
            // DontDestroyOnLoad(gameObject);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Instance = null;
    }
}
