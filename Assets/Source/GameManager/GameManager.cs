using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PrefabProviderInstance prefabProvider = new PrefabProviderInstance();

    public GameManager()
    {
        Instance = this;
    }

    public void Start()
    {
        Ship.InstantiateShip();
    }
}
