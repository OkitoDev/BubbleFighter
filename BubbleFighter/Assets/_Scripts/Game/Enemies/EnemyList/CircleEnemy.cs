using Game.MovementPatterns;
using UnityEngine;

namespace Game.Enemies.EnemyList
{
    public class CircleEnemy : BaseEnemy
    {
        protected override IMovementPattern GetMovementPattern()
        {
            return new AimTowardsPlayer();
        }

        protected override void Attack()
        {
            _projectileSpawner.SpawnProjectile(transform.position, Quaternion.identity, false);
        }
    }
}