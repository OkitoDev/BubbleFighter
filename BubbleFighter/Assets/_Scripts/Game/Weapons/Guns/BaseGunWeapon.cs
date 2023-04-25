using System.Collections.Generic;
using System.Linq;
using Game.Extensions;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public abstract class BaseGunWeapon : MonoBehaviour
    {
        [SerializeField] private GunWeapon gunWeapon;
        private readonly BulletSpawner _bulletSpawner = new BulletSpawner();
        private Transform _baseFirePoint;
        private List<Upgrade> _upgrades = new List<Upgrade>();

        private float _totalBaseDamage => _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BaseDamage).ToList().Sum(upgrade => upgrade.Value);
        private float _damageMultiplierUpgrades => _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BaseDamage).ToList().Sum(upgrade => upgrade.Value);

        private float _totalDamage;
        private float _totalBulletSize;
        private float _totalBulletSpeed;
        private float _totalCooldown;
        
        private void Awake()
        {
            RecalculateAllStats();
            _baseFirePoint = ObjectFinder.Player.FirePoint;
        }

        public void Fire()
        {
            FireBasicBullet(_baseFirePoint);
        }

        private void FireBasicBullet(Transform firePoint) => _bulletSpawner.SpawnBasicBullet(firePoint, gunWeapon.bulletColors.GetRandomElement(), _totalBulletSpeed,_totalBulletSize, _totalDamage);
        
        public void RecalculateDamage()
        {
            float totalBaseDamage =
                _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BaseDamage).ToList()
                    .Sum(upgrade => upgrade.Value) + GlobalValues.GlobalDamage + gunWeapon.baseDamage;
            float totalDamageMultiplier = 
                _upgrades.Where(upgrade => upgrade.Type == UpgradeType.DamageMultiplier).ToList()
                .Sum(upgrade => upgrade.Value) + GlobalValues.GlobalDamageMultiplier + gunWeapon.damageMultiplier;
            
            _totalDamage = totalBaseDamage * totalDamageMultiplier;
        }

        public void RecalculateBulletSize()
        {
            _totalBulletSize =
                _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BulletSize).ToList()
                .Sum(upgrade => upgrade.Value) + GlobalValues.GlobalBulletSize + gunWeapon.bulletSize;;
        }

        public void RecalculateBulletSpeed()
        {
            _totalBulletSpeed =
                _upgrades.Where(upgrade => upgrade.Type == UpgradeType.BulletSpeed).ToList()
                    .Sum(upgrade => upgrade.Value) + GlobalValues.GlobalBulletSpeed + gunWeapon.bulletSpeed;
        }

        public void RecalculateCooldown()
        {
            // TODO
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
    }
}