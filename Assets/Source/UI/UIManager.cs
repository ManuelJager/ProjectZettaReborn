#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> UIObjectPrefabManager;
    public PowerDrawBar powerDrawBar;

    [SerializeField] private Canvas canvas;

    public void Awake()
    {
        canvas.worldCamera = Camera.main;
    }
}
