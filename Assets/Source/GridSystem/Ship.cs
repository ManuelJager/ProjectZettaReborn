using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Ship : BlockGrid
    {
        public Rigidbody2D rb2d;

        public static Ship InstantiateShip(List<GridBlockBase> uBlockList = null)
        {
            var shipObject = new GameObject("Ship");
            var ship = shipObject.AddComponent<Ship>();
            ship.rb2d = shipObject.AddComponent<Rigidbody2D>();
            ship.rb2d.gravityScale = 0f;
            ship.uBlockList = uBlockList ?? ship.defaultGrid;
            return ship;
        }

        public void Update()
        {
            transform.rotation = GridUtilities.MouseLookAtRotation(transform, 200);
        }
    }
}

