using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem.Blocks;

namespace Zetta.GridSystem
{
    public class Ship : BlockGrid
    {
        public Rigidbody2D rb2d;

        [System.Obsolete]
        public static Ship InstantiateShip(List<GridBlockBase> uBlockList)
        {
            var shipObject = new GameObject("Ship");
            var ship = shipObject.AddComponent<Ship>();
            ship.rb2d = shipObject.AddComponent<Rigidbody2D>();
            ship.rb2d.gravityScale = 0f;
            ship.uBlockList = uBlockList;
            return ship;
        }

        public static Ship InstantiateShip(Blueprints.Blueprint blueprint)
        {
            var shipObject = new GameObject("Ship");
            var ship = shipObject.AddComponent<Ship>();
            ship.rb2d = shipObject.AddComponent<Rigidbody2D>();
            ship.rb2d.gravityScale = 0f;
            ship.uBlockList = ship.InstantiateBlueprint(blueprint);
            return ship;
        }
    }
}