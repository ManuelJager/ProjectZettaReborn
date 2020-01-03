using Zetta.GridSystem.Blueprints;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem.Blocks;

namespace Zetta.GridSystem
{
    public partial class BlockGrid : MonoBehaviour
    {
        // Unordered list of references to all blocks in grid
        public List<GridBlockBase> uBlockList = null;

        // The size of the grid
        public Vector2 Size
        {
            get {
                // Create new empty bounds
                Bounds bounds = new Bounds(this.transform.position, Vector3.zero);

                // Get the all bounds of all children
                var renderers = GetComponentsInChildren<Renderer>();

                foreach(Renderer renderer in renderers)
                {
                    // Encalsulate the renderer bounds to the global bounds
                    bounds.Encapsulate(renderer.bounds);
                }
                return new Vector2(
                    (float)System.Math.Floor(bounds.size.x),
                    (float)System.Math.Floor(bounds.size.y));
            }
        }

        public void Awake()
        {
            GridSizeChanged += UpdateCenterOfMass;
        }

        /// <summary>
        /// Instantiates the blueprint and sets the parent to the current block
        /// </summary>
        /// <param name="blueprint">The blueprint to instantiate</param>
        /// <returns>The objects instantiated</returns>
        public List<GridBlockBase> InstantiateBlueprint(Blueprint blueprint)
        {
            uBlockList = GameManager.Instance.bpInstantiator.InstantiateBlueprint(blueprint, transform);
            return uBlockList;
        }
    }
}
