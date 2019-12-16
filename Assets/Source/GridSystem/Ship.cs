using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Ship : BlockGrid
    {
        public static void InstantiateShip(List<GridBlockBase> uBlockList = null)
        {
            var shipObject = new GameObject("Ship");
            var ship = shipObject.AddComponent<Ship>();
            ship.uBlockList = uBlockList ?? ship.defaultGrid;
        }

        public void Update()
        {
            transform.rotation = GridUtilities.MouseLookAtRotation(transform, 100);
        }
    }
}

