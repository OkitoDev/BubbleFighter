using UnityEngine;

namespace Game.Upgrades.UpgradeTrees
{
    [CreateAssetMenu(fileName = "Upgrade Tree", menuName = "Upgrades/Upgrade Trees/Base Damage")]
    public class BaseDamageUpgradeTree : ScriptableObject
    {
        public ValueUpgrades<float> baseDamageUpgrades = new ValueUpgrades<float>();
    }
}