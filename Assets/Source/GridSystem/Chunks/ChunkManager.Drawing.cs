namespace Zetta.GridSystem
{
    public partial class ChunkManager
    {
        public void Start()
        {
            Debugger.DrawChunkBordersChanged += DrawBordersChange;
        }

        /// <summary>
        /// Activates when a new state is set for the draw chunk borders event
        /// </summary>
        /// <param name="newState">The new state of the option</param>
        public void DrawBordersChange(bool newState)
        {
            if (newState == false)
            {
                // Clear all lines
                ChunkDrawer.Instance.ClearChunkBorders();
            }
            else
            {
                // Add all chunks to the drawing list
                ChunkHelper.LoopOverChunks(loadedChunks, (Chunk chunk) =>
                {
                    ChunkDrawer.Instance.DrawChunkBorder(chunk);
                });
            }
        }
    }
}