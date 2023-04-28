using Game.MovementPatterns;
using UnityEngine;

namespace Game.Weapons.Projectiles
{
    public class ProjectileFactory
    {
        private ProjectileData _projectileData;
        private readonly Projectile _projectilePrefab;
        private float _damage;
        private float _sizeMultiplier;
        private float _speedMultiplier;
        private readonly bool _wasCreatedByPlayer;

        public ProjectileFactory(Projectile projectilePrefab, ProjectileData projectileData, float damage, float sizeMultiplier = 1f, float speedMultiplier = 1f, bool wasCreatedByPlayer = false)
        {
            _projectilePrefab = projectilePrefab;
            _wasCreatedByPlayer = wasCreatedByPlayer;
            _projectileData = projectileData;
            _damage = damage;
            _sizeMultiplier = sizeMultiplier;
            _speedMultiplier = speedMultiplier;
        }

        public void CreateProjectile(Vector3 position, Quaternion rotation, IMovementPattern movementPattern, bool colliderTriggerValue, Color? color = null)
        {
            Color newColor = color ?? Color.white;
            Object.Instantiate(_projectilePrefab, position, rotation)
                .SetProjectileData(_projectileData)
                .SetMovementPattern(movementPattern)
                .SetDamage(_damage)
                .SetColor(newColor)
                .SetSizeMultiplier(_sizeMultiplier)
                .SetSpeedMultiplier(_speedMultiplier)
                .SetBulletCreatedByPlayer(_wasCreatedByPlayer)
                .SetColliderTrigger(colliderTriggerValue);
        }

        public void UpdateProjectileData(ProjectileData projectileData)
        {
            _projectileData = projectileData;
        }

        public void UpdateProjectileDamage(float damage)
        {
            _damage = damage;
        }

        public void UpdateSpeedMultiplier(float speedMultiplier)
        {
            _speedMultiplier = speedMultiplier;
        }
        
        public void UpdateSizeMultiplier(float sizeMultiplier)
        {
            _sizeMultiplier = sizeMultiplier;
        }
    }
}