namespace Game.Weapons
{
    public enum UpgradeType
    {
        BaseDamage,
        DamageMultiplier,
        BulletSize,
        BulletSpeed,
        CooldownDecrease
    }
    
    public class Upgrade
    {
        public UpgradeType Type;
        public float Value;
    }
}