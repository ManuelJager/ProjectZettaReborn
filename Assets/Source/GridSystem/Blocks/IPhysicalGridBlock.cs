using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.GridSystem.Blocks
{
    public interface IPhysicalGridBlock
    {
        float Armor { get; set; }
        float Health { get; set; }
        void ApplyDamage(float damage);
    }
}