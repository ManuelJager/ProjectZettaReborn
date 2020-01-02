using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LazySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

    public static T Instance => LazyInstance.Value;

    private static T CreateSingleton()
    {
        var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
        var instance = ownerObject.AddComponent<T>();
        DontDestroyOnLoad(ownerObject);
        return instance;
    }

    // call this from inherited class to initialize
    public static void Echo()
    {
        Debug.Log($"{Instance.GetType().Name} echoe'd");
    }
}
