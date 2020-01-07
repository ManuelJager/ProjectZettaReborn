#pragma warning disable CS0649
#pragma warning disable CS4014

namespace Zetta.UI
{
    public partial class UIManager
    {
        public bool GameplayLayerActiveState
        {
            get => Instance.gameplayLayer.gameObject.activeSelf;
            set => Instance.gameplayLayer.gameObject.SetActive(value);
        }

        public bool DebuggerLayerActiveState
        {
            get => Instance.debuggerLayer.gameObject.activeSelf;
            set => Instance.debuggerLayer.gameObject.SetActive(value);
        }

        public void ToggleDebuggerLayer()
        {
            DebuggerLayerActiveState = !DebuggerLayerActiveState;
            var statusString = DebuggerLayerActiveState ? "ON" : "OFF";
            Zetta.UI.NoticeManager.Instance.Prompt($"Debugger menu is now {statusString}");
        }
    }
}