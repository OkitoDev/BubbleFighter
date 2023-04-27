using UnityEngine;

namespace Game.Weapons.Projectiles.Patterns
{
    /// <summary>
    /// That's a bit hacky but Idc
    /// </summary>
    public abstract class ProjectilePatternSetter
    {
        protected Transform projectileTransform;
        protected float projectileSpeedMultiplier;

        public virtual void SetValues(Transform transform, float speedMultiplier)
        {
            projectileTransform = transform;
            projectileSpeedMultiplier = speedMultiplier;
        }
    }
}