using System;
using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    [Serializable]
    public class FollowPlayer : BaseMovementPattern
    {
        private Transform _playerTransform;
        
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _playerTransform = Services.GetServiceFromComponent<Player.Player>().transform;
        }
        public override void UpdatePosition()
        {
            var position = targetTransform.position;
            
            Vector3 direction = (_playerTransform.position - position).normalized;
            position += direction * targetSpeedMultiplier * Time.deltaTime;
            targetTransform.position = position;
        }

        public override object Clone()
        {
            return new FollowPlayer();
        }
    }
}