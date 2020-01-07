using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.Controllers;
using Zetta.Exceptions;
using Zetta.Generics;

namespace Zetta.GridSystem
{
    public partial class ChunkManager : LazySingleton<ChunkManager>
    {
        public static readonly int CHUNK_SIZE = 16;

        public Chunk[,] loadedChunks;

        public ChunkManager()
        {
            loadedChunks = new Chunk[256, 256];
            Array.Clear(loadedChunks, 0, loadedChunks.Length);
        }

        /// <summary>
        /// Converts the given position into a chunk position
        /// </summary>
        /// <param name="position">The position to convert</param>
        /// <returns>The converted chunk position</returns>
        public Vector2Int GetChunkPosition(Vector2 position)
        {
            int x = position.x > 0 ? (int)System.Math.Floor((position.x + CHUNK_SIZE / 2) / CHUNK_SIZE) : (int)System.Math.Ceiling((position.x - CHUNK_SIZE / 2) / CHUNK_SIZE);
            int y = position.y > 0 ? (int)System.Math.Floor((position.y + CHUNK_SIZE / 2) / CHUNK_SIZE) : (int)System.Math.Ceiling((position.y - CHUNK_SIZE / 2) / CHUNK_SIZE);

            return new Vector2Int(
                x + loadedChunks.GetLength(0) / 2,
                y + loadedChunks.GetLength(1) / 2);
        }
        
        /// <summary>
        /// Gets the world position of the given chunk position
        /// </summary>
        /// <param name="chunkPosition">The chunk position to get the world position of</param>
        /// <returns>A world position</returns>
        public Vector2 GetWorldPosition(Vector2Int chunkPosition)
        {
            return new Vector2(
                -(CHUNK_SIZE * (loadedChunks.GetLength(0) / 2)) + chunkPosition.x * CHUNK_SIZE,
                -(CHUNK_SIZE * (loadedChunks.GetLength(1) / 2)) + chunkPosition.y * CHUNK_SIZE);
        }

        /// <summary>
        /// Adds a chunk to the loaded chunks
        /// </summary>
        /// <param name="chunk">The chunk to add</param>
        public void AddChunk(Chunk chunk)
        {
            loadedChunks[chunk.position.x, chunk.position.y] = chunk;

            // Check if the chunk borders should be drawn
            if(Debugger.DrawChunkBorders)
            {
                ChunkDrawer.Instance.DrawChunkBorder(chunk);
            }
        }

        /// <summary>
        /// Adds an entity to the corrisponding chunk
        /// </summary>
        /// <param name="entity">The entity to add to a chunk</param>
        public void AddEntity(ZettaEntity entity)
        {
            var chunkToAdd = GetOrCreateChunk(entity.CalculatedChunkPosition);

            chunkToAdd.Add(entity);

            // Load more chunks when the entity has been added
            LoadChunks(chunkToAdd.position, SettingsController.CHUNK_RENDER_DISTANCE);
        }

        /// <summary>
        /// Changes an entity's chunk it is in
        /// </summary>
        /// <param name="entity">The entity to change chunk</param>
        /// <param name="toChunk">The chunk to change to</param>
        public void HopChunk(ZettaEntity entity, Vector2Int toChunk)
        {
            // Changing chunks
            var currentChunk = GetOrCreateChunk(entity.ChunkPosition);
            var nextChunk = GetOrCreateChunk(toChunk);

            currentChunk.Remove(entity);
            nextChunk.Add(entity);

            // Fire chunk changed event
            EntityChangedChunk?.Invoke(entity);

            // Unload the unused chunks
            int side = GetSide(currentChunk.position, toChunk);
            UnloadUnusedChunks(toChunk, SettingsController.CHUNK_RENDER_DISTANCE, side);

            // Load new chunks
            LoadChunks(toChunk, SettingsController.CHUNK_RENDER_DISTANCE);
        }

        /// <summary>
        /// Gets the side the chunk is changing to
        /// </summary>
        /// <param name="from">From chunk</param>
        /// <param name="towards">Towards chunk</param>
        /// <returns>The side of the chunk is moving(0=up 1=right 2=down 3=left)</returns>
        private int GetSide(Vector2Int from, Vector2Int towards)
        {
            if (towards.y > from.y) return 0;
            else if (towards.x > from.x) return 1;
            else if (towards.y < from.y) return 2;
            else if (towards.x < from.x) return 3;

            // No side was found(should never happen)
            return -1;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void EchoThis() => Echo();
    }
}
