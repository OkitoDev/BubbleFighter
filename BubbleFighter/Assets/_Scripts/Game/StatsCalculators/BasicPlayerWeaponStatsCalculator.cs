using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Game.Weapons;
using Game.Weapons.Guns;
using UnityEngine;

namespace Game.StatsCalculators
{
    [Serializable]
    public class BasicPlayerWeaponStatsCalculator : IWeaponStatsCalculator
    {
        public float GetTotalDamage(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades)
        {
            float totalBaseDamageFromUpgrades = upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.BaseDamage).ToList().Sum(upgrade => (float) upgrade.Value);
            float totalDamageMultiplierFromUpgrades = Mathf.Max(1f,upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.DamageMultiplier).ToList().Sum(upgrade => (float) upgrade.Value));
            float totalBaseDamage = weaponBaseStats.damage + totalBaseDamageFromUpgrades + GlobalValues.GlobalPlayerDamage;
            float totalDamageMultiplier = weaponBaseStats.damageMultiplier * Mathf.Max(1f, totalDamageMultiplierFromUpgrades) * GlobalValues.GlobalPlayerDamageMultiplier;
            
            return totalBaseDamage * totalDamageMultiplier;
        }

        public float GetTotalCooldown(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades)
        {
            float totalCooldownReductionFromUpgrades = upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.CooldownReduction).Aggregate(1f, (current, upgrade) => current * ((100 - (float) upgrade.Value) * 0.01f));
            return  Mathf.Max(weaponBaseStats.cooldown * GlobalValues.GlobalPlayerCooldownReduction * totalCooldownReductionFromUpgrades,0.001f);
        }

        public float GetTotalProjectileSpeed(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades)
        {
            float totalProjectileSpeedFromUpgrades = Mathf.Max(1f, upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.ProjectileSpeed).ToList().Sum(upgrade => (float) upgrade.Value));
            return weaponBaseStats.projectileSpeed * totalProjectileSpeedFromUpgrades * GlobalValues.GlobalProjectileSpeed;
        }

        public float GetTotalProjectileSize(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades)
        {
            float totalProjectileSizeFromUpgrades = Mathf.Max(1f, upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.ProjectileSize).ToList().Sum(upgrade => (float) upgrade.Value));
            return weaponBaseStats.projectileSize * totalProjectileSizeFromUpgrades * GlobalValues.GlobalProjectileSize;
        }
    }
}