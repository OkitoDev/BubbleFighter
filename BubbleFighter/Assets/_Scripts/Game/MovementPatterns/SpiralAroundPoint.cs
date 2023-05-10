using System;
using UnityEngine;

namespace Game.MovementPatterns
{
    [Serializable]
    public class SpiralMovementData
    {
        public Vector2 initialRadius = new Vector2(0, 0);
        public Vector2 linearVelocity = new Vector2(0, 0);
        public float angularVelocity = 1f;
        public float timeScale = 1f;
        public Vector2 radiusExpansionMultiplier = new Vector2(10,10);
        public bool constantSpeed = false;
    }
    
    [Serializable]
    public class SpiralAroundPoint : BaseMovementPattern
    {
        [SerializeField] private SpiralMovementData spiralMovementData;
        private Vector2 _startingPoint;
        private Vector2 _initial;
        private Vector2 _currentRadius;
        private float _time;

        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _startingPoint = targetTransform.position;
            float angle = spiralMovementData.angularVelocity * _time;
            _initial = new Vector2(Mathf.Cos(angle) * _currentRadius.x,Mathf.Sin(angle) * _currentRadius.y);
        }

        public override void UpdatePosition()
        {
            if (spiralMovementData.constantSpeed)
            {
                MoveObjectInSpiralWayConstantSpeed();
            }
            else
            {
                MoveObjectInSpiralWay();
            }
        }

        public void Initialize(SpiralMovementData movementData)
        {
            spiralMovementData = movementData;
            _currentRadius = movementData.initialRadius;
        }

        public override object Clone()
        {
            var clonedObject = new SpiralAroundPoint();
            clonedObject.Initialize(spiralMovementData);
            return clonedObject;
        }

        private void MoveObjectInSpiralWay()
        {
            float angle = spiralMovementData.angularVelocity * _time;
            var position = spiralMovementData.linearVelocity * _time;
            position += new Vector2(Mathf.Cos(angle) * _currentRadius.x, Mathf.Sin(angle) * _currentRadius.y) + _startingPoint - _initial;
            targetTransform.position = position;
 
            // Adjust time and radius
            _time += spiralMovementData.timeScale * Time.deltaTime;
            _currentRadius += 0.001f * spiralMovementData.radiusExpansionMultiplier;
        }
        
        // TODO not working well yet
        private void MoveObjectInSpiralWayConstantSpeed()
        {
            // TODO Tis doesn't quite work
            float angle = spiralMovementData.angularVelocity * _time;
            var position = spiralMovementData.linearVelocity * _time;
            position += new Vector2(Mathf.Cos(angle) * _currentRadius.x, Mathf.Sin(angle) * _currentRadius.y) + _startingPoint - _initial;
            targetTransform.position = position;

            // Adjust time and radius
            _time += Time.deltaTime;
            float speed = 5f;
            float maxRadius = Mathf.Max(_currentRadius.x, _currentRadius.y);
            float spiralTime = maxRadius / speed;
            spiralMovementData.angularVelocity = 2 * Mathf.PI / spiralTime;
            _currentRadius += 0.001f * spiralMovementData.radiusExpansionMultiplier;
        }
    }
}