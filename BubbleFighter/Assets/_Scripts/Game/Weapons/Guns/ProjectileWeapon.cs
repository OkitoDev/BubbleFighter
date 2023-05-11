using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Game.Audio;
using Game.MovementPatterns;
using Game.Projectiles;
using Game.StatsCalculators;
using Game.Weapons.ScriptableObjects;
using Game.Weapons.SpawnPoints;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public class ProjectileWeapon
    {
        private readonly List<WeaponUpgrade> _upgrades = new List<WeaponUpgrade>();
        private readonly MonoBehaviour _weaponHolder;
        private readonly Transform _baseFirePoint;
        private Sprite _projectileSprite;
        private IProjectileSpawnPointProvider _projectileSpawnPointProvider;
        private IWeaponStatsCalculator _weaponStatsCalculator;
        private IMovementPattern _projectileMovementPattern;
        private WeaponStats _weaponBaseStats;
        private List<Vector3> _projectileSpawnPointsOffsets;
        private ProjectileSpawner _projectileSpawner;
        private bool _autoFire;
        private Coroutine _lastAutoFireRoutine = null;
        
        // Total weapon stats
        private float _totalDamage;
        private float _totalProjectileSize;
        private float _totalProjectileSpeed;
        private float _totalCooldown;
        
        private float _lastFireTime = Mathf.NegativeInfinity;
        private bool IsWeaponOffCooldown => Time.time - _lastFireTime > _totalCooldown;

        public bool AutoFire
        {
            private get => _autoFire;
            set
            {
                _autoFire = value;
                RestartAutoFire(AutoFire);
            }
        }

        public IMovementPattern ProjectilesMovementPattern
        {
            private get => _projectileMovementPattern;
            set
            {
                _projectileMovementPattern = value;
                UpdateProjectileSpawner();
            }
        }

        public IProjectileSpawnPointProvider ProjectileSpawnPointProvider
        {
            private get => _projectileSpawnPointProvider;
            set
            {
                _projectileSpawnPointProvider = value;
                _projectileSpawnPointsOffsets = ProjectileSpawnPointProvider.GetSpawnPointsOffsets();
            }
        }
        
        public IWeaponStatsCalculator WeaponStatsCalculator
        {
            private get => _weaponStatsCalculator;
            set
            {
                _weaponStatsCalculator = value;
                RecalculateAllStats();
            }
        }
        
        public WeaponStats WeaponBaseStats
        {
            private get => _weaponBaseStats;
            set
            {
                _weaponBaseStats = value;
                RecalculateAllStats();
            }
        }
        public BaseProjectile ProjectilePrefab { private get; set; }

        public ProjectileWeapon(MonoBehaviour weaponHolder, ProjectileWeaponData weaponData)
        {
            _weaponHolder = weaponHolder;
            _baseFirePoint = weaponHolder.transform; ;
            GlobalValues.OnGlobalPlayerWeaponStatsChanged += RecalculateAllStats;
            Initialize(weaponData);
            
#if UNITY_EDITOR
            weaponData.OnScriptableObjectChange += Initialize;
#endif
        }

        private void Initialize(ProjectileWeaponData weaponData)
        {
            _projectileSprite = weaponData.projectileSprite;

            var defaultProjectilesMovementPattern = new AimTowardsDirection();
            defaultProjectilesMovementPattern.Initialize(Vector3.right);

            WeaponBaseStats = weaponData.weaponBaseStats;
            ProjectilePrefab = weaponData.projectilePrefab == null ? GameAssets.Instance.prefabBasicProjectile : weaponData.projectilePrefab;
            ProjectilesMovementPattern = weaponData.projectileMovementPattern ?? defaultProjectilesMovementPattern;
            ProjectileSpawnPointProvider = weaponData.projectileSpawnPointProvider ?? new SinglePointInCenter();
            WeaponStatsCalculator = weaponData.weaponStatsCalculator ?? new BasicPlayerWeaponStatsCalculator();
        }

        public void ManualFire()
        {
            if (!IsWeaponOffCooldown) return;
            Fire();
        }

        private void Fire()
        {
            foreach (var offset in _projectileSpawnPointsOffsets)
            {
                SoundManager.PlaySound("TestShoot", _weaponHolder.transform.position);
                _projectileSpawner.SpawnProjectile(_baseFirePoint.position + offset, Quaternion.identity, _weaponBaseStats.colliderTrigger);
            }
            
            _lastFireTime = Time.time;
        }
        
        private IEnumerator FireCoroutine()
        {
            while (AutoFire)
            {
                Fire();
                yield return new WaitForSeconds(_totalCooldown);
            }
        }

        private void RecalculateAllStats()
        {
            if (WeaponStatsCalculator == null) return;
            _totalDamage = WeaponStatsCalculator.GetTotalDamage(WeaponBaseStats, _upgrades);
            _totalProjectileSize = WeaponStatsCalculator.GetTotalProjectileSize(WeaponBaseStats, _upgrades);
            _totalProjectileSpeed = WeaponStatsCalculator.GetTotalProjectileSpeed(WeaponBaseStats, _upgrades);
            _totalCooldown = WeaponStatsCalculator.GetTotalCooldown(WeaponBaseStats, _upgrades);
            UpdateProjectileSpawner();
            RestartAutoFire(AutoFire);
        }

        public void AddUpgrade(WeaponUpgrade weaponUpgrade)
        {
            _upgrades.Add(weaponUpgrade);
            RecalculateAllStats();
        }


        private void RestartAutoFire(bool isEnabled)
        {
            if (_lastAutoFireRoutine != null) _weaponHolder.StopCoroutine(_lastAutoFireRoutine);
            if (isEnabled) _lastAutoFireRoutine = _weaponHolder.StartCoroutine(FireCoroutine());
        }

        private void UpdateProjectileSpawner()
        {
            _projectileSpawner = new ProjectileSpawner(ProjectilePrefab, 
                _totalDamage, ProjectilesMovementPattern, _projectileSprite, _totalProjectileSize, _totalProjectileSpeed, true);
        }
    }
}