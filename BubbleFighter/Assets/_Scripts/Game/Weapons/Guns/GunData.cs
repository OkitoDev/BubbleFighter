using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Weapons.Guns
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun")]
    public class GunData : ScriptableObject
    {
        public float cooldown; // In seconds
        public float projectileSize;
        public float projectileSpeed;
        public float baseDamage;
        public float damageMultiplier;
        public List<Color> projectileColors;
    }
}