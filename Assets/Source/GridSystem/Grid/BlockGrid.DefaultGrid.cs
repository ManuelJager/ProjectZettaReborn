using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public partial class BlockGrid
    {
        public List<GridBlockBase> defaultGrid
        {
            get
            {
                var block = Instantiate(PrefabProvider.GetPrefab("Zetta::LightArmorBlock"));
                block.transform.SetParent(transform);

                return new List<GridBlockBase>
                {
                    (GridBlockBase)block.GetComponent(typeof(GridBlockBase))
                };
            }
        }
    }
}
