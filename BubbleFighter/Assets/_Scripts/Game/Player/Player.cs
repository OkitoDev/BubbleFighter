using System;
using Enums;
using Game.Events;
using Game.MovementPatterns;
using Game.Weapons.Guns;
using Game.Weapons.ScriptableObjects;
using Game.Weapons.SpawnPoints;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : MonoBehaviour
    {
        [SerializeField] [Range(1,10)] private float moveSpeed;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameEvent fireEvent;
        [SerializeField] private ProjectileWeaponData projectileWeaponData;

        public Transform FirePoint => firePoint;


        private Vector2 _moveDirection;
        private Vector2 _mousePosition;
        private Rigidbody2D _rigidbody;
        private ProjectileWeapon _projectileWeapon;

        private int currentMovementPatternUpgrade = 0;
        private int currentProjectileSpawnUpgrade = 0;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _projectileWeapon = new ProjectileWeapon(this, projectileWeaponData);
        }

        private void Update()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            _moveDirection = new Vector2(moveX, moveY).normalized;

            if (Input.GetButtonDown("Fire1"))
            {
                fireEvent.Raise();
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                GlobalValues.DecreaseCooldown(99f);
                GlobalValues.AddDamage(1f);
                GlobalValues.AddDamageMultiplier(1f);
                GlobalValues.AddProjectileSize(10f);
                GlobalValues.AddProjectileSpeed(5000f);
                GlobalValues.AddEnemyHealthMultiplier(10000f);
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                GlobalValues.DecreaseCooldown(5f);
                GlobalValues.AddDamage(100f);
                GlobalValues.AddDamageMultiplier(100f);
                GlobalValues.AddProjectileSize(5f);
                _projectileWeapon.AutoFire = true;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                GlobalValues.AddProjectileSpeed(50f);
            }
            
            if (Input.GetKeyDown(KeyCode.M))
            {
                GlobalValues.AddProjectileSpeed(-50f);
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _projectileWeapon.Upgrade(WeaponUpgradeType.ProjectileFiringPattern, projectileWeaponData.projectileMovementUpgradeTree.projectileMovementUpgrades.upgrades[currentMovementPatternUpgrade].value);
                currentMovementPatternUpgrade++;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _projectileWeapon.Upgrade(WeaponUpgradeType.ProjectileSpawnPoint, projectileWeaponData.projectileSpawnPointsUpgradeTree.projectileSpawnPointUpgrades.upgrades[currentProjectileSpawnUpgrade].value);
                currentProjectileSpawnUpgrade++;
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
            
            Vector2 aimDirection = _mousePosition - _rigidbody.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            //_rigidbody.rotation = aimAngle;
        }
    }
}