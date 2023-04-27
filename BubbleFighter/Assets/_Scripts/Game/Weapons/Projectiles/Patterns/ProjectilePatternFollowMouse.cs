using UnityEngine;

namespace Game.Weapons.Projectiles.Patterns
{
    public class ProjectilePatternFollowMouse : ProjectilePatternSetter, IProjectilePattern
    {

        public void UpdatePosition()
        {
            Vector3 mousePos = Mouse.GetMouseWorldPosition;
            var position = projectileTransform.position;
            
            Vector3 direction = (mousePos - position).normalized;

            projectileTransform.Translate(direction * projectileSpeedMultiplier * Time.deltaTime);
        }
    }
}