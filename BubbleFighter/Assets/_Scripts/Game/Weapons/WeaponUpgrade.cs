using Enums;

namespace Game.Weapons
{
    public class WeaponUpgrade
    {
        public readonly WeaponUpgradeType Type;
        public readonly float Value;

        public WeaponUpgrade(WeaponUpgradeType type, float value)
        {
            Type = type;
            Value = value;
        }
    }
}