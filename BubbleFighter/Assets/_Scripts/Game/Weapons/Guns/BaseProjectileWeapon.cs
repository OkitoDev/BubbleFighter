using System.Collections.Generic;
using System.Linq;
using Enums;
using Game.MovementPatterns;
using Game.Projectiles;
using UnityEngine;
using Utilities;

namespace Game.Weapons.Guns
{
    public abstract class BaseProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private GunData gunData;
        private ProjectileSpawner _projectileSpawner;
        private Transform _baseFirePoint;
        private readonly List<WeaponUpgrade> _upgrades = new List<WeaponUpgrade>();
        private List<Vector3> _projectileSpawnPointsOffsets;

        private float TotalBaseDamageFromUpgrades => _upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.BaseDamage).ToList().Sum(upgrade => upgrade.Value);
        private float TotalDamageMultiplierFromUpgrades => Mathf.Max(1f,_upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.DamageMultiplier).ToList().Sum(upgrade => upgrade.Value));
        private float TotalProjectileSizeFromUpgrades => Mathf.Max(1f, _upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.ProjectileSize).ToList().Sum(upgrade => upgrade.Value));
        private float TotalProjectileSpeedFromUpgrades => Mathf.Max(1f, _upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.ProjectileSpeed).ToList().Sum(upgrade => upgrade.Value));
        private float TotalCooldownReductionFromUpgrades => _upgrades.Where(upgrade => upgrade.Type == WeaponUpgradeType.CooldownReduction).Aggregate(1f, (current, upgrade) => current * ((100 - upgrade.Value) * 0.01f));

        // Stats
        private float _totalDamage;
        private float _totalProjectileSizeMultiplier;
        private float _totalProjectileSpeedMultiplier;
        private float _totalCooldown;
        
        private float _lastFireTime = Mathf.NegativeInfinity;
        private bool IsWeaponOffCooldown => Time.time - _lastFireTime > _totalCooldown;
        private bool _isAutoFireEnabled;

        private void Awake()
        {
            RecalculateAllStats();
            _baseFirePoint = Services.GetServiceFromScene<Player.Player>().FirePoint;
            _projectileSpawnPointsOffsets = GetProjectileSpawnPointsOffsets();
        }

        public void EnableAutoFire()
        {
            _isAutoFireEnabled = true;
            RestartAutoFire();
        }

        public void ManualFire()
        {
            if (!IsWeaponOffCooldown || _isAutoFireEnabled) return;
            
            Fire();
            _lastFireTime = Time.time;
        }

        private void Fire()
        {
            foreach (var offset in _projectileSpawnPointsOffsets)
            {
                _projectileSpawner.SpawnProjectile(_baseFirePoint.position + offset, Quaternion.identity, GetMovementPattern(), gunData.colliderTrigger);
            }
        }
        
        private void RecalculateDamage()
        {
            float totalBaseDamage = gunData.baseDamage + TotalBaseDamageFromUpgrades + GlobalValues.GlobalPlayerDamage;
            float totalDamageMultiplier = gunData.damageMultiplier * Mathf.Max(1f, TotalDamageMultiplierFromUpgrades) * GlobalValues.GlobalPlayerDamageMultiplier;
            
            _totalDamage = totalBaseDamage * totalDamageMultiplier;
        }

        private void RecalculateProjectileSize()
        {
            _totalProjectileSizeMultiplier = gunData.projectileSize * TotalProjectileSizeFromUpgrades * GlobalValues.GlobalProjectileSize;
        }

        private void RecalculateProjectileSpeed()
        {
            _totalProjectileSpeedMultiplier = gunData.projectileSpeed * TotalProjectileSpeedFromUpgrades * GlobalValues.GlobalProjectileSpeed;
        }

        private void RecalculateCooldown()
        {
            _totalCooldown = Mathf.Max(gunData.cooldown * GlobalValues.GlobalPlayerCooldownReduction * TotalCooldownReductionFromUpgrades,0.001f);
            RestartAutoFire();
        }

        public void RecalculateAllStats()
        {
            RecalculateDamage();
            RecalculateProjectileSize();
            RecalculateProjectileSpeed();
            RecalculateCooldown();
            UpdateProjectileFactory();
        }

        public void AddUpgrade(WeaponUpgrade weaponUpgrade)
        {
            _upgrades.Add(weaponUpgrade);
            RecalculateAllStats();
        }

        private void RestartAutoFire()
        {
            if (!_isAutoFireEnabled) return;
            
            CancelInvoke(nameof(Fire));
            InvokeRepeating(nameof(Fire), _totalCooldown, _totalCooldown);
        }
        
        protected abstract IMovementPattern GetMovementPattern();

        protected virtual List<Vector3> GetProjectileSpawnPointsOffsets()
        {
            return new List<Vector3>()
            {
                new Vector3(0, 0)
            };
        }

        private void UpdateProjectileFactory()
        {
            _projectileSpawner = new ProjectileSpawner(GameAssets.Instance.prefabDefaultProjectile
                ,gunData.projectileData, _totalDamage, _totalProjectileSizeMultiplier, _totalProjectileSpeedMultiplier, true);
        }
    }
}