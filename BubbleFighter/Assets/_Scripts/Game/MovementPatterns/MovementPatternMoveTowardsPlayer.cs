using UnityEngine;

namespace Game.MovementPatterns
{
    public class MovementPatternMoveTowardsPlayer : MovementPatternSetter, IMovementPattern
    {
        private Transform _playerTransform;
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _playerTransform = ObjectFinder.Player.transform;
        }
        public void UpdatePosition()
        {
            var position = targetTransform.position;
            
            Vector3 direction = (_playerTransform.position - position).normalized;
            position += direction * targetSpeedMultiplier * Time.deltaTime;
            targetTransform.position = position;
        }
    }
}