using UnityEngine;

namespace Game.MovementPatterns
{
    public class MovementPatternAimTowardsMouse : MovementPatternSetter, IMovementPattern
    {
        private Vector3 _direction;
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _direction = (Mouse.GetMouseWorldPosition - targetTransform.position).normalized;
        }

        public void UpdatePosition()
        { 
            targetTransform.position += _direction * targetSpeedMultiplier * Time.deltaTime;
        }
    }
}