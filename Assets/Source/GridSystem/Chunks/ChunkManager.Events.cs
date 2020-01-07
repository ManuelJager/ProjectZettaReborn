namespace Zetta.GridSystem
{
    public partial class ChunkManager
    {
        public delegate void EntityChangedChunkDelegate(ZettaEntity entity);
        public event EntityChangedChunkDelegate EntityChangedChunk;
    }
}
