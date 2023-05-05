using UnityEngine;

namespace Game.Projectiles
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Ammunition/Projectile")]
    public class ProjectileData : ScriptableObject
    {
        public float size;
        public float lifespan;
        public Sprite sprite;
    }
}