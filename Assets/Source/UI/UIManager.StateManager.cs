#pragma warning disable CS0649

public partial class UIManager
{
    public bool GameplayLayerActiveState
    {
        get => gameplayLayer.activeSelf;
        set => gameplayLayer.SetActive(value);
    }

    public bool DebuggerLayerActiveState
    {
        get => debuggerLayer.activeSelf;
        set => debuggerLayer.SetActive(value);
    }

    public void ToggleDebuggerLayer()
    {
        DebuggerLayerActiveState = !DebuggerLayerActiveState;
    }
}
