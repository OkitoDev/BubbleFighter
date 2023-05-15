using Enums;
using Game.MovementPatterns;
using Game.Weapons.SpawnPoints;
using UnityEngine;

namespace Game.Upgrades.UpgradeTrees
{
    [CreateAssetMenu(fileName = "Upgrade Tree", menuName = "Upgrades/Upgrade Trees/Projectile Spawn Points")]
    public class ProjectileSpawnPointsUpgradeTree : ScriptableObject
    {
        public ReferenceUpgrades<IProjectileSpawnPointProvider> projectileSpawnPointUpgrades = new ReferenceUpgrades<IProjectileSpawnPointProvider>();
    }
}