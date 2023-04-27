using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]
    public class EnemyData : ScriptableObject
    {
        public float worth;
        [Min(1f)] public float healthPoints = 1;
        public float movementSpeed;
        public float attackCooldown;
        public float collisionCooldown = 0.5f;
        public float collisionDamageMultiplier;
        public float damage;
        public Sprite sprite;
        public List<EnemyVariant> variants = new List<EnemyVariant>()
        {
            new EnemyVariant{enemyType = EnemyType.Normal},
            new EnemyVariant{enemyType = EnemyType.Assassin},
            new EnemyVariant{enemyType = EnemyType.Tank},
            new EnemyVariant{enemyType = EnemyType.MiniBoss}
        };
    }
}