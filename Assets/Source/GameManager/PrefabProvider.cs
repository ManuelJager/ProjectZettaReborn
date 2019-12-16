using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabProvider
{
    public static GameObject GetPrefab(string index)
    {
        return PrefabProviderInstance.Instance[index];
    }
}

[System.Serializable]
public class PrefabProviderInstance : SerializableDictionary<string, GameObject>
{
    public static PrefabProviderInstance Instance;

    public PrefabProviderInstance()
    {
        Instance = this;
    }
}

