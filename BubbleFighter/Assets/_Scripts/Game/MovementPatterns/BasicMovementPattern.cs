using UnityEngine;

namespace Game.MovementPatterns
{
    public class BasicMovementPattern : MovementPatternSetter, IMovementPattern
    {
        private readonly Vector3 _direction;

        public BasicMovementPattern(Vector3 direction)
        {
            _direction = direction.normalized;
        }

        public void UpdatePosition()
        {
            targetTransform.position += _direction * targetSpeedMultiplier * Time.deltaTime;
        }
    }
}