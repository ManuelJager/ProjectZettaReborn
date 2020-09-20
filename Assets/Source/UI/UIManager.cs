#pragma warning disable CS0649

using UnityEngine;
using Zetta.Generics;
using Zetta.InputWrapper;
using Zetta.UI.Controllers;
using Zetta.UI.UIWindows;

namespace Zetta.UI
{
    public partial class UIManager : AutoInstanceMonoBehaviour<UIManager>
    {
        public GameplayLayer gameplayLayer;
        public DebuggerLayer debuggerLayer;
        public UIWindowTabManager pauseMenuLayer;
        public Canvas canvas;

        public new void Awake()
        {
            base.Awake();
        }

        public void Start()
        {
            canvas.worldCamera = Camera.main;
            DebuggerLayerActiveState = false;
            InputManager.ClickF10 += ToggleDebuggerLayer;
            InputManager.ClickEsc += TogglePauseMenuLayer;
        }
    }
}