using System;
using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    [Serializable]
    public class AimTowardsMouse : BaseMovementPattern
    {
        private Vector3 _direction;
        
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _direction = (Vector2)(MouseUtils.GetMouseWorldPosition - targetTransform.position);
            _direction = _direction.normalized;
        }

        public override void UpdatePosition()
        { 
            targetTransform.position += _direction * targetSpeedMultiplier * Time.deltaTime;
        }

        public override object Clone()
        {
            return new AimTowardsMouse();
        }
    }
}