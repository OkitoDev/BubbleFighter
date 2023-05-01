using Game.MovementPatterns;

namespace Game.Enemies.EnemyList
{
    public class CircleEnemy : BaseEnemy
    {
        protected override IMovementPattern GetMovementPattern()
        {
            return new MovementPatternMoveTowardsPlayer();
        }

        protected override void Attack()
        {
            //Debug.Log($"{enemyType} is attacking for {_totalDamage} damage!");
        }
    }
}