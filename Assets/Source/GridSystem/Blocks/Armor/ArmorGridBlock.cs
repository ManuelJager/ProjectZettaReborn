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
        public float MaxArmor { get => 10f; }

        [SerializeField]
        private float health;

        public float Health
        {
            get => health;
            set => health = value;
        }
        public float MaxHealth { get => 10f; }

        public void ApplyDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }
}