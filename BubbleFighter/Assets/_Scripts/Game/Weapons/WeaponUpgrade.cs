using Enums;

namespace Game.Weapons
{
    public class WeaponUpgrade
    {
        public readonly WeaponUpgradeType Type;
        public readonly object Value;

        public WeaponUpgrade(WeaponUpgradeType type, object value)
        {
            Type = type;
            Value = value;
        }
    }
}