using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.GridSystem.Blocks;
using Zetta.UI;

namespace Zetta.GridSystem
{
    public class ZettaEntity : BlockGrid
    {
        private Vector2Int chunkPosition;

        public Vector2Int ChunkPosition
        {
            get => chunkPosition;
            set => chunkPosition = value;
        }

        public Vector2Int CalculatedChunkPosition
        {
            get => ChunkManager.Instance.GetChunkPosition(transform.position);
        }

        public float Health
        {
            get
            {
                float totalHealth = 0;
                for(int i = 0; i < uBlockList.Count; i++)
                {
                    if(uBlockList[i] is IPhysicalGridBlock)
                    {
                        var physicalGridBlock = (IPhysicalGridBlock)uBlockList[i];
                        totalHealth += physicalGridBlock.Health;
                    }
                }
                return totalHealth;
            }
        }

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
