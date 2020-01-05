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
    // TODO: Fix chunk aligning

    public partial class ChunkManager : LazySingleton<ChunkManager>
    {
        public static readonly int CHUNK_SIZE = 16;

        public List<Chunk> loadedChunks;

        public ChunkManager()
        {
            loadedChunks = new List<Chunk>();

            // ChunkManager.Drawing
            linesToDraw = new List<Vector3>();
        }

        public void Update()
        {
            // ChunkManager.Drawing
            lineRenderer.positionCount = linesToDraw.Count;
            for (int i = 0; i < linesToDraw.Count; i++)
            {
                Vector3 line = linesToDraw[i];
                lineRenderer.SetPosition(i, line);
            }
        }

        /// <summary>
        /// Loads all chunks in a certain radius
        /// </summary>
        /// <param name="radius">The radius of the chunks that should be loaded in</param>
        public void LoadChunks(Vector2Int origin, int radius)
        {

            // TODO: Change the loaded to a circle not to a square
            for(int x = -(radius / 2); x < radius / 2; x++)
            {
                for(int y = -(radius / 2); y < radius / 2; y++)
                {
                    GetOrCreateChunk(new Vector2Int(x + origin.x, y + origin.y));
                }
            }
            
        }

        // TODO: Create an unloading chunks method

        /// <summary>
        /// Adds a chunk to the loaded chunks
        /// </summary>
        /// <param name="chunk">The chunk to add</param>
        public void AddChunk(Chunk chunk)
        {
            loadedChunks.Add(chunk);

            // Check if the chunk borders should be drawn
            if(Debugger.DrawChunkBorders)
            {
                DrawChunkBorders(chunk);
            }
        }

        /// <summary>
        /// Adds an entity to the corrisponding chunk
        /// </summary>
        /// <param name="entity">The entity to add to a chunk</param>
        public void AddEntity(ZettaEntity entity)
        {
            var chunkToAdd = GetOrCreateChunk(entity.ChunkPosition);

            chunkToAdd.AddEntity(entity);

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

            currentChunk.RemoveEntity(entity);
            nextChunk.AddEntity(entity);

            // Fire chunk changed event
            EntityChangedChunk?.Invoke(entity);

            // TODO: Make old chunks unload
            LoadChunks(toChunk, SettingsController.CHUNK_RENDER_DISTANCE);
        }

        /// <summary>
        /// Tries to find a chunk at the given position
        /// </summary>
        /// <param name="position">The position of the chunk to get</param>
        /// <returns>The found chunk</returns>
        public Chunk GetChunk(Vector2Int position)
        {
            // Finds the position in all loaded chunks
            var chunk = loadedChunks.Find(c => c.position.Equals(position));

            return chunk;
        }

        protected Chunk GetOrCreateChunk(Vector2Int chunkPosition)
        {
            var chunkToAdd = GetChunk(chunkPosition);

            // Create the chunk if it doesn't exist
            if (chunkToAdd == null)
            {
                chunkToAdd = new Chunk(chunkPosition);
                AddChunk(chunkToAdd);
                Debug.Log($"Created chunk on {chunkPosition}");
            }

            return chunkToAdd;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void EchoThis() => Echo();
    }
}
