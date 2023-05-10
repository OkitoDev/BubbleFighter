using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.MovementPatterns
{
    [Serializable]
    public class CircleAroundPoint : BaseMovementPattern
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private float radius;
        [SerializeField] private float rotationSpeed;
        private Vector3 _initialPosition;

        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            offset = targetTransform.position - _initialPosition;
        }

        public override void UpdatePosition()
        {
            targetTransform.position = _initialPosition + offset;
            
            targetTransform.RotateAround(_initialPosition, Vector3.forward, rotationSpeed * Time.deltaTime);
            offset = targetTransform.position - _initialPosition;
            
            targetTransform.position += offset.normalized * radius;
        }

        public void Initialize(float rotateSpeed, float circleRadius, Vector3 initialPosition)
        {
            rotationSpeed = rotateSpeed;
            radius = circleRadius;
            _initialPosition = initialPosition;
        }

        public override object Clone()
        {
            var clonedObject = new CircleAroundPoint();
            clonedObject.Initialize(rotationSpeed, radius, _initialPosition);
            return clonedObject;
        }
    }
}