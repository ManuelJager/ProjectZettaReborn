#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<GameObject> UIObjectPrefabManager;
    public PowerDrawBar powerDrawBar;
    public HealthDrawBar healthDrawBar;
    public GameObject debugLayer;

    public static UIManager Instance;

    [SerializeField] private Canvas canvas;

    public UIManager()
    {
        Instance = this;
    }

    public void Awake()
    {
        canvas.worldCamera = Camera.main;
        DebugLayerActiveState = false;
        InputManager.ClickF10 += ToggleDebugger;
    }

    public bool DebugLayerActiveState
    {
        get => debugLayer.activeSelf;
        set => debugLayer.SetActive(value);
    }

    public void ToggleDebugger()
    {
        DebugLayerActiveState = !DebugLayerActiveState;
    }
}
