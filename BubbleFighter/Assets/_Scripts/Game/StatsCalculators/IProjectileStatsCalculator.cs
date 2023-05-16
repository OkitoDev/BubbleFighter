using System.Collections.Generic;
using Game.Weapons;
using Game.Weapons.Guns;

namespace Game.StatsCalculators
{
    public interface IProjectileStatsCalculator
    {
        public float GetTotalProjectileSpeed(WeaponStats weaponBaseStats, IEnumerable<WeaponUpgrade> upgrades);
        public float GetTotalProjectileSize(WeaponStats weaponBaseStats, IEnumerable<WeaponUpgrade> upgrades);
    }
}