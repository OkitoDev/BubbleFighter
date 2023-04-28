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
            new EnemyVariant{enemyType = EnemyType.Normal, cooldownDecrease = 0f, damageMultiplier = 1f, sizeMultiplier = 1f, worthMultiplier = 1f, healthPointsMultiplier = 1f, movementSpeedMultiplier = 1f, color = Color.white},
            new EnemyVariant{enemyType = EnemyType.Tank, cooldownDecrease = 0f, damageMultiplier = 1f, sizeMultiplier = 2f, worthMultiplier = 1.5f, healthPointsMultiplier = 3f, movementSpeedMultiplier = 0.75f, color = Color.green},
            new EnemyVariant{enemyType = EnemyType.Assassin, cooldownDecrease = 10f, damageMultiplier = 1.5f, sizeMultiplier = 0.75f, worthMultiplier = 1.5f, healthPointsMultiplier = 0.75f, movementSpeedMultiplier = 2f, color = Color.red},
            new EnemyVariant{enemyType = EnemyType.MiniBoss, cooldownDecrease = 50f, damageMultiplier = 3f, sizeMultiplier = 5f, worthMultiplier = 5f, healthPointsMultiplier = 5f, movementSpeedMultiplier = 0.75f, color = Color.gray},
        };
    }
}