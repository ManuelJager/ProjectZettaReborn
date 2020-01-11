using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem.Blocks;
using Zetta.GridSystem.Blueprints;

namespace Zetta.GridSystem
{
    public partial class BlockGrid : MonoBehaviour
    {
        // Unordered list of references to all blocks in grid
        public List<GridBlockBase> uBlockList = null;

        public bool rendering;

        public Blueprint blueprint;

        public bool Rendering
        {
            get => rendering;
            set
            {
                // Grab all renderers of this blockgrid
                var renderers = GetComponentsInChildren<Renderer>();
                for (int i = 0; i < renderers.Length; i++)
                {
                    var renderer = renderers[i];

                    // Set the rendering state of the renderer
                    renderer.enabled = value;
                }
                // Set the global rendering state
                rendering = value;
            }
        }

        // The size of the grid
        public Vector2 Size
        {
            get
            {
                var bounds = Bounds;
                return new Vector2(
                    (float)System.Math.Floor(bounds.size.x),
                    (float)System.Math.Floor(bounds.size.y));
            }
        }

        protected Bounds Bounds
        {
            get
            {
                // Create new empty bounds
                Bounds bounds = new Bounds(transform.position, Vector3.zero);

                // Get the all bounds of all children
                var renderers = GetComponentsInChildren<Renderer>();

                foreach (Renderer renderer in renderers)
                {
                    // Encalsulate the renderer bounds to the global bounds
                    bounds.Encapsulate(renderer.bounds);
                }
                return bounds;
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
            return GameManager.Instance.bpInstantiator.InstantiateBlueprint(blueprint, transform);
        }

        /// <summary>
        /// Instantiates the blueprint and sets the parent to the current block
        /// </summary>
        /// <param name="blueprint">The blueprint to instantiate</param>
        /// <returns>The objects instantiated</returns>
        public List<GridBlockBase> InstantiateBlueprint(Blueprint blueprint, Transform transform)
        {
            return GameManager.Instance.bpInstantiator.InstantiateBlueprint(blueprint, transform);
        }
    }
}