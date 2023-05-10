using Game.MovementPatterns;
using UnityEngine;

namespace Game.Projectiles
{
    public class ProjectileSpawner
    {
        private readonly BaseProjectile _projectilePrefab;
        private readonly float _damage;
        private readonly float _sizeMultiplier;
        private readonly float _speedMultiplier;
        private readonly bool _wasCreatedByPlayer;
        private readonly Sprite _sprite;
        private IMovementPattern _movementPattern;
        private float _projectileLifespan = 5f;

        // TODO That's just a quick way to do it, redo this later
        private static Transform _projectilesParent;

        public ProjectileSpawner(BaseProjectile projectilePrefab, float damage, IMovementPattern movementPattern, Sprite sprite, float sizeMultiplier = 1f, float speedMultiplier = 1f, bool wasCreatedByPlayer = false)
        {
            if (_projectilesParent == null)
            {
                _projectilesParent = new GameObject("Projectiles").transform;
            }
            
            _projectilePrefab = projectilePrefab;
            _wasCreatedByPlayer = wasCreatedByPlayer;
            _damage = damage;
            _sizeMultiplier = sizeMultiplier;
            _speedMultiplier = speedMultiplier;
            _movementPattern = movementPattern;
            _sprite = sprite;
        }

        public void SpawnProjectile(Vector3 position, Quaternion rotation, bool colliderTrigger, Color? color = null)
        {
            var newMovementPatternInstance = (IMovementPattern) _movementPattern.Clone();
            Color newColor = color ?? Color.white;
            var projectile = Object.Instantiate(_projectilePrefab, position, rotation, _projectilesParent);
            projectile.SetMovementPattern(newMovementPatternInstance);
            projectile.SetDamage(_damage);
            projectile.SetColor(newColor);
            projectile.SetSizeMultiplier(_sizeMultiplier);
            projectile.SetSpeedMultiplier(_speedMultiplier);
            projectile.SetBulletCreatedByPlayer(_wasCreatedByPlayer);
            projectile.SetColliderTrigger(colliderTrigger);
            projectile.SetLifespan(_projectileLifespan);
            projectile.SetSprite(_sprite);
        }

        public void UpdateMovementPattern(IMovementPattern movementPattern)
        {
            _movementPattern = movementPattern;
        }

        public void UpdateProjectileLifespan(float lifespan)
        {
            _projectileLifespan = lifespan;
        }
    }
}