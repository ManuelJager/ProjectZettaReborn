using System.Collections.Generic;
using UnityEngine;
using Zetta.Generics;

namespace Zetta.GridSystem
{
    public class ChunkDrawer : AutoInstanceMonoBehaviour<ChunkDrawer>
    {
        private List<(Vector2, Vector2, Color)> drawList;

        public ChunkDrawer()
        {
            drawList = new List<(Vector2, Vector2, Color)>();
        }

        /// <summary>
        /// Draws the borders of a given chunk
        /// </summary>
        /// <param name="chunk">The chunk to draw</param>
        public void DrawChunkBorder(Chunk chunk)
        {
            Vector2[] corners = ChunkHelper.GetChunkWorldPositionCorners(chunk);

            drawList.Add((corners[0], corners[1], Color.blue));
            drawList.Add((corners[1], corners[2], Color.blue));
            drawList.Add((corners[2], corners[3], Color.blue));
            drawList.Add((corners[3], corners[0], Color.blue));
        }

        /// <summary>
        /// Removes the chunk border(makes it red)
        /// </summary>
        /// <param name="chunk">The chunk to remove</param>
        public void RemoveChunkBorder(Chunk chunk)
        {
            Vector2[] corners = ChunkHelper.GetChunkWorldPositionCorners(chunk);

            ChangeCornerColor((corners[0], corners[1], Color.blue), Color.red);
            ChangeCornerColor((corners[1], corners[2], Color.blue), Color.red);
            ChangeCornerColor((corners[2], corners[3], Color.blue), Color.red);
            ChangeCornerColor((corners[3], corners[0], Color.blue), Color.red);
        }

        /// <summary>
        /// Clears all chunk border draws
        /// </summary>
        public void ClearChunkBorders()
        {
            drawList.Clear();
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < drawList.Count; i++)
            {
                var toDraw = drawList[i];
                Debug.DrawLine(toDraw.Item1, toDraw.Item2, toDraw.Item3);
            }
        }

        /// <summary>
        /// Changes the color of a corner to the given color
        /// </summary>
        /// <param name="corner">The corner to change</param>
        /// <param name="color">The color to change the corner color to</param>
        private void ChangeCornerColor((Vector2, Vector2, Color) corner, Color color)
        {
            (Vector2, Vector2, Color) to = (corner.Item1, corner.Item2, color);
            drawList[drawList.FindIndex(i => i.Equals(corner))] = to;
        }
    }
}