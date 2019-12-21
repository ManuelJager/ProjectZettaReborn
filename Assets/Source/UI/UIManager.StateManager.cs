#pragma warning disable CS0649

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
    }
}
