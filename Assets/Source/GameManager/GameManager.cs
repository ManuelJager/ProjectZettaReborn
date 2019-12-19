using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using Blueprints;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PrefabProviderInstance prefabProvider = new PrefabProviderInstance();

    [System.NonSerialized]
    public BlueprintInstantiator bpInstantiator;

    public GameManager()
    {
        Instance = this;
    }

    public void Awake()
    {
        // Create an instance of the blueprint instantiator
        bpInstantiator = GetComponent<BlueprintInstantiator>();
    }
}
