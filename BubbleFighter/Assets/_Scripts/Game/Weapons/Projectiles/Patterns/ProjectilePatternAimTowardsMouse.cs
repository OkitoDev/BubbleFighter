using UnityEngine;

namespace Game.Weapons.Projectiles.Patterns
{
    public class ProjectilePatternAimTowardsMouse : ProjectilePatternSetter, IProjectilePattern
    {
        private Vector3 _direction;
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _direction = (Mouse.GetMouseWorldPosition - projectileTransform.position).normalized;
        }

        public void UpdatePosition()
        { 
            projectileTransform.position += _direction * projectileSpeedMultiplier * Time.deltaTime;
        }
    }
}