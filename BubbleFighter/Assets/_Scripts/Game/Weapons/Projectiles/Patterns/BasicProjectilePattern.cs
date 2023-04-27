using UnityEngine;

namespace Game.Weapons.Projectiles.Patterns
{
    public class BasicProjectilePattern : ProjectilePatternSetter, IProjectilePattern
    {
        private readonly Vector3 _direction;

        public BasicProjectilePattern(Vector3 direction)
        {
            _direction = direction.normalized;
        }

        public void UpdatePosition()
        {
            projectileTransform.position += _direction * projectileSpeedMultiplier * Time.deltaTime;
        }
    }
}