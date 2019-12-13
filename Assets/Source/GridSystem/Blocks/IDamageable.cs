using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageableBlock
{
    float Armor { get; set; }
    float Health { get; set; }
    void ApplyDamage(float damage);
}
