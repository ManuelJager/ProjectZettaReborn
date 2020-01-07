#pragma warning disable CS0649

using UnityEngine;
using Zetta.InputWrapper;
using Zetta.UI.Controllers;
using Zetta.UI.UIWindows;

namespace Zetta.UI
{
    public partial class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameplayLayer gameplayLayer;
        public DebuggerLayer debuggerLayer;
        public UIWindowTabManager pauseMenuLayer;
        public Canvas canvas;

        public UIManager()
        {
            Instance = this;
        }

        public void Awake()
        {
            canvas.worldCamera = Camera.main;
            DebuggerLayerActiveState = false;
            InputManager.ClickF10 += ToggleDebuggerLayer;
            InputManager.ClickEsc += TogglePauseMenuLayer;
        }
    }
}