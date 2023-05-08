using Game.MovementPatterns;
using UnityEngine;

namespace Game.Enemies.EnemyList
{
    public class CircleEnemy : BaseEnemy
    {
        protected override IMovementPattern GetMovementPattern()
        {
            return new MovementPatternFollowPlayer();
        }

        protected override void Attack()
        {
            _projectileSpawner.SpawnProjectile(transform.position, Quaternion.identity, GetProjectileMovementPattern(), true);
        }
    }
}