using System;
using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    [Serializable]
    public class AimTowardsPlayer : BaseMovementPattern
    {
        private Vector3 _initialDirection;
        
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            var playerTransform = Services.GetServiceFromComponent<Player.Player>().transform;
            _initialDirection = (playerTransform.position - targetTransform.position).normalized;
        }
        public override void UpdatePosition()
        {
            var position = targetTransform.position;
            
            position += _initialDirection * targetSpeedMultiplier * Time.deltaTime;
            targetTransform.position = position;
        }

        public override object Clone()
        {
            return new AimTowardsPlayer();
        }
    }
}