using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]
    public class EnemyData : ScriptableObject
    {
        public float worth;
        public float healthPoints;
        public float movementSpeed;
        public float attackCooldown;
        public float collisionCooldown = 0.5f;
        public float collisionDamageMultiplier;
        public float damage;
        public Sprite sprite;
        public List<EnemyVariants> variants = new List<EnemyVariants>();
    }
}