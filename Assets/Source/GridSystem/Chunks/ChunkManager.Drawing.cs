using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.Drawing;

namespace Zetta.GridSystem
{
    public partial class ChunkManager
    {
        private LineRenderer lineRenderer;
        private List<Vector3> linesToDraw;

        public void Start()
        {
            // Initialize the line renderer
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            // Set the line renderer settings
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.02f;
            lineRenderer.positionCount = 0;

            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;

            Debugger.DrawChunkBordersChanged += DrawBordersChange;
        }

        /// <summary>
        /// Activates when a new state is set for the draw chunk borders event
        /// </summary>
        /// <param name="newState">The new state of the option</param>
        public void DrawBordersChange(bool newState)
        {
            if(newState == false)
            {
                // Clear all lines
                linesToDraw.Clear();
            } else
            {
                // Add all chunks to the drawing list
                for(int i = 0; i < loadedChunks.Count; i++)
                {
                    var chunk = loadedChunks[i];
                    DrawChunkBorders(chunk);
                }
            }
        }

        /// <summary>
        /// Adds the speficied chunk to the drawing list
        /// </summary>
        /// <param name="chunk">The chunk to add</param>
        // NOTE: This does not work flawlessly yet but it is good enough for debugging for now
        public void DrawChunkBorders(Chunk chunk)
        {
            Vector2[] borders = ChunkHelper.GetChunkWorldPositionCorners(chunk);
            for (int i = 0; i < borders.Length; i++)
            {
                linesToDraw.Add(borders[i]);
            }
            linesToDraw.Add(borders[0]);
        }

    }
}
