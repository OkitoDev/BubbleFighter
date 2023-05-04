using UnityEngine;

namespace Game.MovementPatterns
{
    public class SpiralMovementData
    {
        public Vector2 initialRadius = new Vector2(0, 0);
        public Vector2 linearVelocity = new Vector2(0, 0);
        public float angularVelocity = 1f;
        public float timeScale = 1f;
        public Vector2 radiusExpansionMultiplier = new Vector2(10,10);
        public bool constantSpeed = false;
    }
    
    public class MovementPatternSpiralAroundPoint : MovementPatternSetter, IMovementPattern
    {
        private Vector2 _startingPoint;
        private Vector2 _initial;
        private readonly SpiralMovementData _spiralMovementData;
        private Vector2 _radius;
        private float _time;

        public MovementPatternSpiralAroundPoint(SpiralMovementData spiralMovementData)
        {
            _spiralMovementData = spiralMovementData;
            _radius = _spiralMovementData.initialRadius;
        }

        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            _startingPoint = targetTransform.position;
            float angle = _spiralMovementData.angularVelocity * _time;
            _initial = new Vector2(Mathf.Cos(angle) * _radius.x,Mathf.Sin(angle) * _radius.y);
        }

        public void UpdatePosition()
        {
            if (_spiralMovementData.constantSpeed)
            {
                MoveObjectInSpiralWayConstantSpeed();
            }
            else
            {
                MoveObjectInSpiralWay();
            }
        }

        private void MoveObjectInSpiralWay()
        {
            float angle = _spiralMovementData.angularVelocity * _time;
            var position = _spiralMovementData.linearVelocity * _time;
            position += new Vector2(Mathf.Cos(angle) * _radius.x, Mathf.Sin(angle) * _radius.y) + _startingPoint - _initial;
            targetTransform.position = position;
 
            // Adjust time and radius
            _time += _spiralMovementData.timeScale * Time.deltaTime;
            _radius += 0.001f * _spiralMovementData.radiusExpansionMultiplier;
        }
        
        // TODO not working well yet
        private void MoveObjectInSpiralWayConstantSpeed()
        {
            // TODO Tis doesn't quite work
            float angle = _spiralMovementData.angularVelocity * _time;
            var position = _spiralMovementData.linearVelocity * _time;
            position += new Vector2(Mathf.Cos(angle) * _radius.x, Mathf.Sin(angle) * _radius.y) + _startingPoint - _initial;
            targetTransform.position = position;

            // Adjust time and radius
            _time += Time.deltaTime;
            float speed = 5f;
            float maxRadius = Mathf.Max(_radius.x, _radius.y);
            float spiralTime = maxRadius / speed;
            _spiralMovementData.angularVelocity = 2 * Mathf.PI / spiralTime;
            _radius += 0.001f * _spiralMovementData.radiusExpansionMultiplier;
        }
    }
}