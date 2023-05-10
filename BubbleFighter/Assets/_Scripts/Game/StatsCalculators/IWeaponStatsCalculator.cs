using System.Collections.Generic;
using Game.Weapons;
using Game.Weapons.Guns;

namespace Game.StatsCalculators
{
    // This can be split into different interfaces, but for now it's fine
    public interface IWeaponStatsCalculator
    {
        public float GetTotalDamage(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades);
        public float GetTotalCooldown(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades);
        public float GetTotalProjectileSpeed(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades);
        public float GetTotalProjectileSize(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades);
    }
}