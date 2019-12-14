using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

public class PlayerController
{
    public Ship ship;

    public static PlayerController Instance;
    public PlayerController()
    {
        Instance = this;
    }
}
