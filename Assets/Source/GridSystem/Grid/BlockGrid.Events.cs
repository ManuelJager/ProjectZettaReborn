#pragma warning disable CS0067

namespace Zetta.GridSystem
{
    public partial class BlockGrid
    {
        public delegate void GridSizeChangedDelegate();

        public event GridSizeChangedDelegate GridSizeChanged;
    }
}