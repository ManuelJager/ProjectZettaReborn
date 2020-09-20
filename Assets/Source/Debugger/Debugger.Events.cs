namespace Zetta
{
    public static partial class Debugger
    {
        public delegate void DrawChunkBordersChangedDelegate(bool newState);

        public static event DrawChunkBordersChangedDelegate DrawChunkBordersChanged;
    }
}