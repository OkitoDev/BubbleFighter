using UnityEngine;

namespace Game.MovementPatterns
{

    public class MovementPatternCircleAroundPoint : MovementPatternSetter, IMovementPattern
    {
        private Vector3 _offset;
        private float _radius;
        private readonly Vector3 _initialPosition;
        private readonly float _rotateSpeed;

        public MovementPatternCircleAroundPoint(float rotateSpeed, float radius, Vector3 initialPosition)
        {
            _rotateSpeed = rotateSpeed;
            _radius = radius;
            _initialPosition = initialPosition;
        }

        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _offset = targetTransform.position - _initialPosition;
        }

        public void UpdatePosition()
        {
            targetTransform.position = _initialPosition + _offset;
            
            targetTransform.RotateAround(_initialPosition, Vector3.forward, _rotateSpeed * Time.deltaTime);
            _offset = targetTransform.position - _initialPosition;
            
            targetTransform.position += _offset.normalized * _radius;
        }
    }
}