using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace GridSystem
{
    public class ArmorGridBlock : GridBlockBase, IPhysicalGridBlock
    {
        public ArmorGridBlock(Vector2 size, float mass, float armor, float health)
            : base(size, mass)
        {
            this.armor = armor;
            this.health = health;
        }

        [SerializeField]
        private float armor;
        public float Armor
        {
            get => armor;
            set => armor = value;
        }

        [SerializeField]
        private float health;
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
}

