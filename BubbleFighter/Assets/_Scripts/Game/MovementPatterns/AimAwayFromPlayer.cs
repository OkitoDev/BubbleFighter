using System;
using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    [Serializable]
    public class AimAwayFromPlayer : BaseMovementPattern
    {
        private Transform _playerTransform;
        private Vector3 _initialDirection;
        
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _playerTransform = Services.GetServiceFromComponent<Player.Player>().transform;
            _initialDirection = (targetTransform.position - _playerTransform.position).normalized;
        }
        
        public override void UpdatePosition()
        {
            var position = targetTransform.position;
            
            position += _initialDirection * targetSpeedMultiplier * Time.deltaTime;
            targetTransform.position = position;
        }
        
        public override object Clone()
        {
            return new AimAwayFromPlayer();
        }
    }
}