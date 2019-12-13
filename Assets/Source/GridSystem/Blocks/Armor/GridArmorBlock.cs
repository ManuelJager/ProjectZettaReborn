using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GridArmorBlock : GridBlockBase, IDamageableBlock
{
    public GridArmorBlock(Vector2 size, float armor, float health, float mass)
        : base(size, mass)
    {
        this.armor = armor;
        this.health = health;
    }

    [SerializeField]
    public float armor;
    public float Armor 
    { 
        get => armor; 
        set => armor = value; 
    }

    [SerializeField]
    public float health;
    public float Health 
    { 
        get => health; 
        set => health = value; 
    }

    public void ApplyDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}
