using UnityEngine;

namespace Game.MovementPatterns
{
    // TODO
    public class MovementPatternSpiralAroundPoint : MovementPatternSetter, IMovementPattern
    {
        private Vector3 _offset;
        private float _radius;
        private readonly Vector3 _point;
        private readonly float _rotateSpeed;

        public MovementPatternSpiralAroundPoint(float rotateSpeed, float radius, Vector3 point)
        {
            _rotateSpeed = rotateSpeed;
            _radius = radius;
            _point = point;
        }

        public void UpdatePosition()
        {

        }
    }
}