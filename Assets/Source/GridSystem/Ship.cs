using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem.Blocks;

namespace Zetta.GridSystem
{
    public class Ship : ZettaEntity
    {
        public Rigidbody2D rb2d;

        public static Ship InstantiateShip(Blueprints.Blueprint blueprint)
        {
            // Create the ship object/entity
            var shipObject = new GameObject("Ship");
            var ship = shipObject.AddComponent<Ship>();
            ship.blueprint = blueprint;
            ship.rb2d = shipObject.AddComponent<Rigidbody2D>();
            ship.rb2d.gravityScale = 0f;
            ship.uBlockList = ship.InstantiateBlueprint(blueprint);
            Debug.Log(ship.Size);

            // Add the entity to a chunk
            ChunkManager.Instance.AddEntity(ship);
            return ship;
        }

        public static Ship InstantiateShip(Blueprints.Blueprint blueprint, Transform parent)
        {
            // Create the ship object/entity
            var shipObject = new GameObject("Ship");
            var ship = shipObject.AddComponent<Ship>();
            ship.blueprint = blueprint;
            ship.rb2d = shipObject.AddComponent<Rigidbody2D>();
            ship.rb2d.gravityScale = 0f;
            ship.uBlockList = ship.InstantiateBlueprint(blueprint, parent);

            // Add the entity to a chunk
            ChunkManager.Instance.AddEntity(ship);
            return ship;
        }
    }
}