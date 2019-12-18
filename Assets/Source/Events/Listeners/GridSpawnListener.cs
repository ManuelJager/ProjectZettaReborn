using Events;
using GridSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Listeners
{
    public class GridSpawnListener
    {

        [GridSpawnEvent]
        public void OnSpawn(GridSpawnEvent e)
        {
            // Check if the grid is a ship
            if(e.Grid.GetType() == typeof(Ship))
            {
                // Set the camera zoom
                Vector2 size = e.Grid.Size;
                CameraController.Instance.ZoomCamera(size, true);
            }

        }
    }
}
