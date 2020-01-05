using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.GridSystem.Blocks;
using Zetta.UI;

namespace Zetta.GridSystem
{
    public class ZettaEntity : BlockGrid
    {
        public float Health
        {
            get
            {
                float totalHealth = 0;
                for(int i = 0; i < uBlockList.Count; i++)
                {
                    if(uBlockList[i] is IPhysicalGridBlock)
                    {
                        var physicalGridBlock = (IPhysicalGridBlock)uBlockList[i];
                        totalHealth += physicalGridBlock.Health;
                    }
                }
                return totalHealth;
            }
        }

        public float Armor
        {
            get
            {
                float totalArmor = 0;
                for (int i = 0; i < uBlockList.Count; i++)
                {
                    if (uBlockList[i] is IPhysicalGridBlock)
                    {
                        var physicalGridBlock = (IPhysicalGridBlock)uBlockList[i];
                        totalArmor += physicalGridBlock.Armor;
                    }
                }
                return totalArmor;
            }
        }

        /// <summary>
        /// Checks the visibilty of the current entity and enables or disables it if it is visible
        /// </summary>
        public void CheckVisibilty()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main); //TODO Change camera
            bool isVisible = GeometryUtility.TestPlanesAABB(planes, Bounds);

            // Only change the rendering if it isn't already in that state
            if(isVisible != Rendering)
            {
                Rendering = isVisible;
            }
        }
    }
}
