namespace Zetta.GridSystem.Blocks
{
    public interface IPhysicalGridBlock
    {
        float Armor { get; set; }
        float MaxArmor { get; }
        float Health { get; set; }
        float MaxHealth { get; }

        void ApplyDamage(float damage);
    }
}