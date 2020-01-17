using UnityEngine;
using Zetta.GridSystem.Blueprints;

namespace Zetta.GridSystem
{
    public class Ship : ZettaEntity
    {
        public Rigidbody2D rb2d;

        public static Ship InstantiateShip(BlueprintModel blueprint)
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

        public static Ship InstantiateShip(BlueprintModel blueprint, Transform parent)
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