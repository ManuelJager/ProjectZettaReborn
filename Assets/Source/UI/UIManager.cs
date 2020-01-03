#pragma warning disable CS0649

using UnityEngine;
using Zetta.InputWrapper;
using Zetta.UI.Controllers;

namespace Zetta.UI
{
    public partial class UIManager : MonoBehaviour
    {
        public GameplayLayer gameplayLayer;
        public DebuggerLayer debuggerLayer;

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
}