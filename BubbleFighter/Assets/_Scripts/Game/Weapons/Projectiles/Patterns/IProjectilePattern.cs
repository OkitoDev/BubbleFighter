using UnityEngine;

namespace Game.Weapons.Projectiles.Patterns
{
    public interface IProjectilePattern
    {
        public void UpdatePosition();
        public void SetValues(Transform transform, float speedMultiplier);
    }
}