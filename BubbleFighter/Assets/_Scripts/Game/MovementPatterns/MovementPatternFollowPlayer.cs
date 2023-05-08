using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    public class MovementPatternFollowPlayer : MovementPatternSetter, IMovementPattern
    {
        private Transform _playerTransform;
        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _playerTransform = Services.GetServiceFromComponent<Player.Player>().transform;
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