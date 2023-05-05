using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    public class MovementPatternAimTowardsMouse : MovementPatternSetter, IMovementPattern
    {
        private Vector3 _direction;
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _direction = (Vector2)(MouseUtils.GetMouseWorldPosition - targetTransform.position);
            _direction = _direction.normalized;
        }

        public void UpdatePosition()
        { 
            targetTransform.position += _direction * targetSpeedMultiplier * Time.deltaTime;
        }
    }
}