using Enums;
using Game.MovementPatterns;
using UnityEngine;

namespace Game.Upgrades.UpgradeTrees
{
    [CreateAssetMenu(fileName = "Upgrade Tree", menuName = "Upgrades/Upgrade Trees/Projectile Movement")]
    public class ProjectileMovementUpgradeTree : ScriptableObject
    {
        public ReferenceUpgrades<IMovementPattern> projectileMovementUpgrades = new ReferenceUpgrades<IMovementPattern>();
    }
    
}