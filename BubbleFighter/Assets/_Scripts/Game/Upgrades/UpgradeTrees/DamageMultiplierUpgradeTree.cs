using Enums;
using UnityEngine;

namespace Game.Upgrades.UpgradeTrees
{
    [CreateAssetMenu(fileName = "Upgrade Tree", menuName = "Upgrades/Upgrade Trees/Damage Multiplier")]
    public class DamageMultiplierUpgradeTree : ScriptableObject
    {
        public ValueUpgrades<float> damageMultiplierUpgrades = new ValueUpgrades<float>();
    }
}