#pragma warning disable CS0649
using UnityEngine;

namespace Zetta.GridSystem.Blocks
{
    public class ArmorGridBlock : GridBlockBase, IPhysicalGridBlock
    {
        [SerializeField]
        private float armor;

        public float Armor
        {
            get => armor;
            set => armor = value;
        }

        [SerializeField]
        private float maxArmor;

        public float MaxArmor
        {
            get => maxArmor;
        }

        [SerializeField]
        private float health;

        public float Health
        {
            get => health;
            set => health = value;
        }

        [SerializeField]
        private float maxHealth;

        public float MaxHealth
        {
            get => maxHealth;
        }

        public void ApplyDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}