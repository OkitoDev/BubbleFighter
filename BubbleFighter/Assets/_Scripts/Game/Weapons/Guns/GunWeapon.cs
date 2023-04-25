using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Weapons.Guns
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun")]
    public class GunWeapon : ScriptableObject
    {
        public float cooldown;
        public float bulletSize;
        public float bulletSpeed;
        public float baseDamage;
        public float damageMultiplier;
        public List<Color> bulletColors;
    }
}