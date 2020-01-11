using UnityEngine;
using Zetta.GridSystem.Blocks;

namespace Zetta.GridSystem
{
    public class ZettaEntity : BlockGrid
    {
        private Vector2Int chunkPosition;

        ~ZettaEntity()
        {
            ChunkManager.Instance.RemoveEntity(this);
        }

        public Vector2Int ChunkPosition
        {
            get => chunkPosition;
            set => chunkPosition = value;
        }

        public Vector2Int CalculatedChunkPosition
        {
            get => ChunkManager.Instance.GetChunkPosition(transform.position);
        }

        // TODO: Cache and listen for change events.
        // TODO: Add uBlockList loop to the place where this is used
        public float Health
        {
            get
            {
                float totalHealth = 0;
                for (int i = 0; i < uBlockList.Count; i++)
                {
                    if (uBlockList[i] is IPhysicalGridBlock)
                    {
                        var physicalGridBlock = (IPhysicalGridBlock)uBlockList[i];
                        totalHealth += physicalGridBlock.Health;
                    }
                }
                return totalHealth;
            }
        }

        // TODO: Cache and listen for change events.
        // TODO: Add uBlockList loop to the place where this is used
        public float Armor
        {
            get
            {
                float totalArmor = 0;
                for (int i = 0; i < uBlockList.Count; i++)
                {
                    if (uBlockList[i] is IPhysicalGridBlock)
                    {
                        var physicalGridBlock = (IPhysicalGridBlock)uBlockList[i];
                        totalArmor += physicalGridBlock.Armor;
                    }
                }
                return totalArmor;
            }
        }

        public void FixedUpdate()
        {
            Vector2Int nextChunkPosition = CalculatedChunkPosition;

            if (nextChunkPosition != ChunkPosition)
            {
                ChunkManager.Instance.HopChunk(this, nextChunkPosition);
            }

            ChunkPosition = nextChunkPosition;
        }
    }
}