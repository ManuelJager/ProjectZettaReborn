using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zetta.GridSystem
{
    public static class ChunkHelper
    {
        /// <summary>
        /// Gets the positions of the corners of an chunk
        /// </summary>
        /// <param name="chunk">The chunk to get the corners of</param>
        /// <returns>The corner positions of the chunk</returns>
        public static Vector2[] GetChunkWorldPositionCorners(Chunk chunk)
        {
            Vector2 position = ChunkManager.Instance.GetWorldPosition(chunk.position);
            Vector2 c1 = new Vector2(
                position.x - ChunkManager.CHUNK_SIZE / 2,
                position.y + ChunkManager.CHUNK_SIZE / 2);

            Vector2 c2 = new Vector2(
                position.x + ChunkManager.CHUNK_SIZE / 2,
                position.y + ChunkManager.CHUNK_SIZE / 2);

            Vector2 c3 = new Vector2(
                position.x + ChunkManager.CHUNK_SIZE / 2,
                position.y - ChunkManager.CHUNK_SIZE / 2);

            Vector2 c4 = new Vector2(
                position.x - ChunkManager.CHUNK_SIZE / 2,
                position.y - ChunkManager.CHUNK_SIZE / 2);

            return new Vector2[4] { c1, c2, c3, c4 };
        }

        /// <summary>
        /// Executes the action for all valid chunks in the given array
        /// </summary>
        /// <param name="chunks">An 2d array of the chunks to loop over</param>
        /// <param name="callback">The action to execute every chunk</param>
        public static void LoopOverChunks(Chunk[,] chunks, Action<Chunk> callback)
        {
            for(int x = 0; x < chunks.GetLength(0); x++)
            {
                for (int y = 0; y < chunks.GetLength(0); y++)
                {
                    Chunk chunk = chunks[x, y];

                    if(chunk != null)
                    {
                        callback.Invoke(chunk);
                    }
                }
            }
        }
    }
}
