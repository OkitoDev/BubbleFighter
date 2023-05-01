using UnityEngine;

namespace Game.MovementPatterns
{
    public class MovementPatternCircleAroundPlayer : MovementPatternSetter, IMovementPattern
    {
        private readonly float _rotateSpeed;
        private readonly float _radius;
        private readonly Transform _player;
        private Vector3 _offset;

        public MovementPatternCircleAroundPlayer(float radius, float rotateSpeed)
        {
            _player = ObjectFinder.Player.transform;
            _radius = radius;
            _rotateSpeed = rotateSpeed;
        }

        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _offset = targetTransform.position - _player.position;
        }

        public void UpdatePosition()
        {
            var playerPosition = _player.position;
            targetTransform.position = playerPosition + _offset;
            
            targetTransform.RotateAround(playerPosition, Vector3.forward, _rotateSpeed * Time.deltaTime);
            _offset = targetTransform.position - playerPosition;
            
            targetTransform.position += _offset.normalized * _radius;
        }
    }
}