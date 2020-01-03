using System.Collections.Generic;
using UnityEngine;
using Zetta.Exceptions;
using Zetta.GridSystem.Blocks;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintInstantiator : MonoBehaviour
    {
        /// <summary>
        /// Calculates the new block position
        /// If the size is even, there is no middle so it adds 0.5 to the position
        /// </summary>
        /// <param name="original">The original position</param>
        /// <param name="size">The size of the block</param>
        /// <returns>The new block position</returns>
        private Vector2 GetBlockPosition(Vector2 original, Vector2 size)
        {
            // Check if the x size is even
            if (size.x != 0 && System.Math.Abs(size.x) % 2 == 0)
                original.x += 0.5f;

            // Check if the x size is even
            if (size.y != 0 && System.Math.Abs(size.y) % 2 == 0)
                original.y -= 0.5f;
            return original;
        }

        /// <summary>
        /// Instantiates a blueprint into the game
        /// </summary>
        /// <param name="blueprint">The blueprint to instantiate</param>
        /// <param name="parent">The parent transform of the blueprint</param>
        /// <returns></returns>
        public List<GridBlockBase> InstantiateBlueprint(Blueprint blueprint, Transform parent)
        {
            List<GridBlockBase> blocks = new List<GridBlockBase>();

            // Loop over each block in the blueprint
            foreach (BlueprintBlock blueprintBlock in blueprint.Blocks)
            {
                // Instantiate the block by prefab block type
                try
                {
                    var block = Instantiate(GameManager.PrefabProvider.GetPrefab(blueprintBlock.BlockTypeID));
                    GridBlockBase blockBase = (GridBlockBase)block.GetComponent(typeof(GridBlockBase));
                    // Set the parent of the block
                    block.transform.SetParent(parent);

                    // Set the position of the block
                    Vector2 position = GetBlockPosition(blueprintBlock.VectorPosition, blockBase.Size);
                    block.transform.localPosition = new Vector3(
                        position.x,
                        position.y,
                        0);

                    // Set the rotation of the block
                    float zRotation = blueprintBlock.Rotation * -90f;
                    block.transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);

                    // Add the block to the list
                    blocks.Add(blockBase);
                }
                catch (PrefabNotFoundException) { }
            }

            return blocks;
        }
    }
}