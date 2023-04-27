using Enums;
using UnityEngine;

namespace Game.Enemies
{
    [System.Serializable]
    public struct EnemyVariant
    {
        public EnemyType enemyType;
        public Color color;

        [Range(0.5f, 10f)] public float healthPointsMultiplier;
        [Range(0.5f, 10f)] public float worthMultiplier;
        [Range(0.5f, 10f)] public float movementSpeedMultiplier;
        [Range(0.5f, 10f)] public float damageMultiplier;
        [Range(0.5f, 10f)] public float sizeMultiplier;
        [Range(0f, 99f)] public float cooldownDecrease;
    }
}