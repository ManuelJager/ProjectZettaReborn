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
        /// Converts the given position into a chunk position
        /// </summary>
        /// <param name="position">The position to convert</param>
        /// <returns>The converted chunk position</returns>
        public static Vector2Int GetChunkPosition(Vector2 position)
        {
            float x = position.x == 0 ? .1f : position.x;
            float y = position.y == 0 ? .1f : position.y;

            return new Vector2Int(
                (int)System.Math.Floor(x / ChunkManager.CHUNK_SIZE),
                (int)System.Math.Floor(y / ChunkManager.CHUNK_SIZE));
        }

        public static Vector2 GetWorldPosition(Vector2Int chunkPosition)
        {
            return new Vector2(
                chunkPosition.x * ChunkManager.CHUNK_SIZE,
                chunkPosition.y * ChunkManager.CHUNK_SIZE);
        }

        public static Vector2[] GetChunkWorldPositionCorners(Chunk chunk)
        {
            Vector2 position = GetWorldPosition(chunk.position);
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
    }
}
