using Enums;

namespace Game.Weapons
{
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