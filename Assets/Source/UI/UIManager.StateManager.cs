#pragma warning disable CS0649
#pragma warning disable CS4014

namespace Zetta.UI
{
    public partial class UIManager
    {
        public static bool GameplayLayerActiveState
        {
            get => Instance.gameplayLayer.gameObject.activeSelf;
            set => Instance.gameplayLayer.gameObject.SetActive(value);
        }

        public static bool DebuggerLayerActiveState
        {
            get => Instance.debuggerLayer.gameObject.activeSelf;
            set => Instance.debuggerLayer.gameObject.SetActive(value);
        }

        public static bool PauseMenuLayerActiveState
        {
            get => Instance.pauseMenuLayer.gameObject.activeSelf;
            set => Instance.pauseMenuLayer.gameObject.SetActive(value);
        }

        public static void ToggleDebuggerLayer()
        {
            DebuggerLayerActiveState = !DebuggerLayerActiveState;
            var statusString = DebuggerLayerActiveState ? "ON" : "OFF";
            Zetta.UI.NoticeManager.Instance.Prompt($"Debugger is now {statusString}");
        }

        public static void TogglePauseMenuLayer()
        {
            PauseMenuLayerActiveState = !PauseMenuLayerActiveState;
        }
    }
}