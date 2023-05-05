using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    public class MovementPatternAwayFromPlayer : MovementPatternSetter, IMovementPattern
    {
        private Transform _playerTransform;
        private Vector3 _initialDirection;
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _playerTransform = Services.GetServiceFromScene<Player.Player>().transform;
            _initialDirection = (targetTransform.position - _playerTransform.position).normalized;
        }
        public void UpdatePosition()
        {
            var position = targetTransform.position;
            
            position += _initialDirection * targetSpeedMultiplier * Time.deltaTime;
            targetTransform.position = position;
        }
    }
}