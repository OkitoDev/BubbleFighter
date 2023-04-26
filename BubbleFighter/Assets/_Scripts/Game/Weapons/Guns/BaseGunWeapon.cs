using System.Collections.Generic;
using System.Linq;
using Game.Extensions;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public abstract class BaseGunWeapon : MonoBehaviour
    {
        [SerializeField] private GunData gunData;
        private readonly BulletSpawner _bulletSpawner = new BulletSpawner();
        private Transform _baseFirePoint;
        private readonly List<Upgrade> _upgrades = new List<Upgrade>();

        private float TotalBaseDamageFromUpgrades => _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BaseDamage).ToList().Sum(upgrade => upgrade.Value);
        private float TotalDamageMultiplierFromUpgrades => Mathf.Max(1f,_upgrades.Where(upgrade => upgrade.Type == UpgradeType.DamageMultiplier).ToList().Sum(upgrade => upgrade.Value));
        private float TotalBulletSizeFromUpgrades => Mathf.Max(1f, _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BulletSize).ToList().Sum(upgrade => upgrade.Value));
        private float TotalBulletSpeedFromUpgrades => Mathf.Max(1f, _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BulletSpeed).ToList().Sum(upgrade => upgrade.Value));
        private float TotalCooldownReductionFromUpgrades => _upgrades.Where(upgrade => upgrade.Type == UpgradeType.CooldownReduction).Aggregate(1f, (current, upgrade) => current * ((100 - upgrade.Value) * 0.01f));

        // Stats
        private float _totalDamage;
        private float _totalBulletSize;
        private float _totalBulletSpeed;
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
            
            FireBasicBullet(_baseFirePoint);
            _lastFireTime = Time.time;
        }

        private void FireBasicBullet(Transform firePoint) => _bulletSpawner.SpawnBasicBullet(firePoint, Mouse.GetMouseWorldPosition - ObjectFinder.Player.FirePoint.position, gunData.bulletColors.GetRandomElement(), _totalBulletSpeed,_totalBulletSize, _totalDamage);
        
        public void RecalculateDamage()
        {
            float totalBaseDamage = gunData.baseDamage + TotalBaseDamageFromUpgrades + GlobalValues.GlobalDamage;
            float totalDamageMultiplier = gunData.damageMultiplier * Mathf.Max(1f, TotalDamageMultiplierFromUpgrades) * GlobalValues.GlobalDamageMultiplier;
            
            _totalDamage = totalBaseDamage * totalDamageMultiplier;
        }

        public void RecalculateBulletSize()
        {
            _totalBulletSize = gunData.bulletSize * TotalBulletSizeFromUpgrades * GlobalValues.GlobalBulletSize;
        }

        public void RecalculateBulletSpeed()
        {
            _totalBulletSpeed = gunData.bulletSpeed * TotalBulletSpeedFromUpgrades * GlobalValues.GlobalBulletSpeed;
        }

        public void RecalculateCooldown()
        {
            _totalCooldown = Mathf.Max(gunData.cooldown * GlobalValues.GlobalCooldownReduction * TotalCooldownReductionFromUpgrades,0.001f);
            StartAutoFire();
        }

        public void RecalculateAllStats()
        {
            RecalculateDamage();
            RecalculateBulletSize();
            RecalculateBulletSpeed();
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
            FireBasicBullet(_baseFirePoint);
        }
    }
}