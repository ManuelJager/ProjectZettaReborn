#pragma warning disable CS0649

using UnityEngine;
using Zetta.InputWrapper;
using Zetta.UI.Controllers;
using Zetta.UI.UIWindows;
using Zetta.Generics;

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
            canvas.worldCamera = Camera.main;
            DebuggerLayerActiveState = false;
            InputManager.ClickF10 += ToggleDebuggerLayer;
            InputManager.ClickEsc += TogglePauseMenuLayer;
        }
    }
}