namespace Game.Weapons
{
    public enum UpgradeType
    {
        BaseDamage,
        DamageMultiplier,
        BulletSize,
        BulletSpeed,
        CooldownReduction
    }
    
    public class Upgrade
    {
        public UpgradeType Type;
        public float Value;

        public Upgrade(UpgradeType type, float value)
        {
            Type = type;
            Value = value;
        }
    }
}