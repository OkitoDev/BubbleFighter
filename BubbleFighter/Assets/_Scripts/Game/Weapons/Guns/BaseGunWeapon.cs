using System.Collections.Generic;
using System.Linq;
using Enums;
using Extensions;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public abstract class BaseGunWeapon : MonoBehaviour
    {
        [SerializeField] private GunData gunData;
        private readonly ProjectileSpawner _projectileSpawner = new ProjectileSpawner();
        private Transform _baseFirePoint;
        private readonly List<Upgrade> _upgrades = new List<Upgrade>();

        private float TotalBaseDamageFromUpgrades => _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BaseDamage).ToList().Sum(upgrade => upgrade.Value);
        private float TotalDamageMultiplierFromUpgrades => Mathf.Max(1f,_upgrades.Where(upgrade => upgrade.Type == UpgradeType.DamageMultiplier).ToList().Sum(upgrade => upgrade.Value));
        private float TotalProjectileSizeFromUpgrades => Mathf.Max(1f, _upgrades.Where(upgrade => upgrade.Type == UpgradeType.ProjectileSize).ToList().Sum(upgrade => upgrade.Value));
        private float TotalProjectileSpeedFromUpgrades => Mathf.Max(1f, _upgrades.Where(upgrade => upgrade.Type == UpgradeType.ProjectileSpeed).ToList().Sum(upgrade => upgrade.Value));
        private float TotalCooldownReductionFromUpgrades => _upgrades.Where(upgrade => upgrade.Type == UpgradeType.CooldownReduction).Aggregate(1f, (current, upgrade) => current * ((100 - upgrade.Value) * 0.01f));

        // Stats
        private float _totalDamage;
        private float _totalProjectileSize;
        private float _totalProjectileSpeed;
        private float _totalCooldown;
        
        private float _lastFireTime = Mathf.NegativeInfinity;
        private bool IsWeaponOffCooldown => Time.time - _lastFireTime > _totalCooldown;
        private bool _isAutoFireEnabled;
        
        private void Awake()
        {
            RecalculateAllStats();
            _baseFirePoint = ObjectFinder.Player.FirePoint;
        }

        public void EnableAutoFire()
        {
            _isAutoFireEnabled = true;
            StartAutoFire();
        }

        public virtual void Fire()
        {
            if (!IsWeaponOffCooldown || _isAutoFireEnabled) return;
            
            FireBubbleProjectile(_baseFirePoint);
            _lastFireTime = Time.time;
        }

        private void FireBubbleProjectile(Transform firePoint) => _projectileSpawner.SpawnBubbleProjectile(firePoint, 
            Mouse.GetMouseWorldPosition - ObjectFinder.Player.FirePoint.position, gunData.projectileColors.GetRandomElement(), 
            _totalProjectileSpeed,_totalProjectileSize, _totalDamage, true);
        
        public void RecalculateDamage()
        {
            float totalBaseDamage = gunData.baseDamage + TotalBaseDamageFromUpgrades + GlobalValues.GlobalPlayerDamage;
            float totalDamageMultiplier = gunData.damageMultiplier * Mathf.Max(1f, TotalDamageMultiplierFromUpgrades) * GlobalValues.GlobalPlayerDamageMultiplier;
            
            _totalDamage = totalBaseDamage * totalDamageMultiplier;
        }

        public void RecalculateProjectileSize()
        {
            _totalProjectileSize = gunData.projectileSize * TotalProjectileSizeFromUpgrades * GlobalValues.GlobalProjectileSize;
        }

        public void RecalculateProjectileSpeed()
        {
            _totalProjectileSpeed = gunData.projectileSpeed * TotalProjectileSpeedFromUpgrades * GlobalValues.GlobalProjectileSpeed;
        }

        public void RecalculateCooldown()
        {
            _totalCooldown = Mathf.Max(gunData.cooldown * GlobalValues.GlobalPlayerCooldownReduction * TotalCooldownReductionFromUpgrades,0.001f);
            StartAutoFire();
        }

        public void RecalculateAllStats()
        {
            RecalculateDamage();
            RecalculateProjectileSize();
            RecalculateProjectileSpeed();
            RecalculateCooldown();
        }

        public void AddUpgrade(Upgrade upgrade)
        {
            _upgrades.Add(upgrade);
            RecalculateAllStats();
        }

        private void StartAutoFire()
        {
            if (!_isAutoFireEnabled) return;
            
            CancelInvoke(nameof(AutoFire));
            InvokeRepeating(nameof(AutoFire), _totalCooldown, _totalCooldown);
        }

        private void AutoFire()
        {
            FireBubbleProjectile(_baseFirePoint);
        }
    }
}