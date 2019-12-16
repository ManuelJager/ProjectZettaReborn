using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public partial class BlockGrid
    {
        public Vector2 centerOfMass;
        public void UpdateCenterOfMass()
        {
            var x = 0f; 
            var y = 0f;
            // add weighted mass to vector
            // weighted mass is calculated with distance by mass 
            for (int i = 0; i < uBlockList.Count; i++)
            {
                var local = uBlockList[i].transform.localPosition;
                var mass = uBlockList[i].Mass;
                x += mass * local.x;
                y += mass * local.y;
            }
            centerOfMass = new Vector2(x, y);
        }
    }
}

