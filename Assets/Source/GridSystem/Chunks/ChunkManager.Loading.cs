using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.GridSystem
{
    public partial class ChunkManager
    {
        /// <summary>
        /// Loads all chunks in a certain radius
        /// </summary>
        /// <param name="radius">The radius of the chunks that should be loaded in</param>
        public void LoadChunks(Vector2Int origin, int radius)
        {
            // Loads chunks in the given radius
            for (int x = -(radius); x <= radius; x++)
            {
                for (int y = -(radius); y <= radius; y++)
                {
                    GetOrCreateChunk(new Vector2Int(x + origin.x, y + origin.y));
                }
            }
        }

        /// <summary>
        /// Unloads the chunk at the given position
        /// </summary>
        /// <param name="chunkPosition">The position of the chunk to unload</param>
        public void UnloadChunk(Vector2Int chunkPosition)
        {
            Chunk chunk = GetChunk(chunkPosition);
            if (chunk != null)
            {
                loadedChunks[chunk.position.x, chunk.position.y] = null;

                // Check if it has to unregister the chunk borders
                if (Debugger.DrawChunkBorders)
                {
                    ChunkDrawer.Instance.RemoveChunkBorder(chunk);
                }
            }
        }

        /// <summary>
        /// Unloads the unused chunks
        /// </summary>
        /// <param name="origin">The origin of the chunk</param>
        /// <param name="radius">The radius of chunks loaded</param>
        /// <param name="side">The opposite side of the chunks to unload</param>
        public void UnloadUnusedChunks(Vector2Int origin, int radius, int side)
        {
            List<Chunk> toUnload = new List<Chunk>();
            if (side == 0 || side == 2)
            {
                int originY = side == 0 ?
                    origin.y - radius - 1 :
                    origin.y + radius + 1;

                for (int x = -radius; x <= radius; x++)
                {
                    Chunk chunk = GetChunk(new Vector2Int(origin.x + x, originY));
                    if (chunk != null)
                    {
                        toUnload.Add(chunk);
                    }
                }
            }
            else if (side == 1 || side == 3)
            {
                int originX = side == 1 ?
                    origin.x - radius - 1 :
                    origin.x + radius + 1;

                for (int y = -radius; y <= radius; y++)
                {
                    Chunk chunk = GetChunk(new Vector2Int(originX, origin.y + y));
                    if (chunk != null)
                    {
                        toUnload.Add(chunk);
                    }
                }
            }

            for (int i = 0; i < toUnload.Count; i++)
            {
                UnloadChunk(toUnload[i].position);
            }
        }

        /// <summary>
        /// Tries to find a chunk at the given position
        /// </summary>
        /// <param name="position">The position of the chunk to get</param>
        /// <returns>The found chunk</returns>
        public Chunk GetChunk(Vector2Int position)
        {
            // Finds the position in all loaded chunks
            try
            {
                return loadedChunks[position.x, position.y];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to get a chunk and creates it if it doesn't exist
        /// </summary>
        /// <param name="chunkPosition">The chunk position of</param>
        /// <returns>The chunk retrieved or created</returns>
        private Chunk GetOrCreateChunk(Vector2Int chunkPosition)
        {
            var chunkToAdd = GetChunk(chunkPosition);

            // Create the chunk if it doesn't exist
            if (chunkToAdd == null)
            {
                chunkToAdd = new Chunk(chunkPosition);
                AddChunk(chunkToAdd);
            }

            return chunkToAdd;
        }
    }
}