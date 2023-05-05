using System.Collections.Generic;
using Game.Projectiles;
using UnityEngine;

namespace Game.Weapons.Guns
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun")]
    public class GunData : ScriptableObject
    {
        [Min(0.001f)] public float cooldown = 1f; // In seconds
        [Min(1f)] public float baseDamage = 1f;
        [Min(1f)] public float damageMultiplier = 1f;
        [Min(0.001f)] public float projectileSize = 1f;
        public float projectileSpeed = 1f;
        public bool colliderTrigger;
        public ProjectileData projectileData;
        public List<Color> projectileColors = new List<Color>()
        {
            new Color(255,255,255,255)
        };
    }
}