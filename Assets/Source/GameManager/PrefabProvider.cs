using Exceptions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabProvider
{
    public static GameObject GetPrefab(string index)
    {
        try
        {
            return PrefabProviderInstance.Instance[index];
        }
        catch (KeyNotFoundException)
        {
            throw new PrefabNotFoundException(index);
        }
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

