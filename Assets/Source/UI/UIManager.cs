#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIManager : MonoBehaviour
{
    public List<GameObject> UIObjectPrefabManager;
    public PowerDrawBar powerDrawBar;
    public IntegrityDrawBar integrityDrawBar;

    public GameObject debuggerLayer;
    public GameObject gameplayLayer;

    public static UIManager Instance;

    [SerializeField] private Canvas canvas;

    public UIManager()
    {
        Instance = this;
    }

    public void Awake()
    {
        canvas.worldCamera = Camera.main;
        DebuggerLayerActiveState = false;
        InputManager.ClickF10 += ToggleDebuggerLayer;
    }
}
