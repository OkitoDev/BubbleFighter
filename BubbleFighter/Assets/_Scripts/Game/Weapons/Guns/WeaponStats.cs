using UnityEngine;

namespace Game.Weapons.Guns
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Stats")]
    public class WeaponStats : ScriptableObject
    {
        [Min(0.001f)] public float cooldown = 1f; // In seconds
        [Min(1f)] public float damage = 1f;
        [Min(1f)] public float damageMultiplier = 1f;
        [Min(0.001f)] public float projectileSize = 1f;
        public float projectileSpeed = 1f;
        public bool colliderTrigger = false;
    }
}