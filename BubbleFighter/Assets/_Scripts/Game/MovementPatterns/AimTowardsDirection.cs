using System;
using UnityEngine;

namespace Game.MovementPatterns
{
    [Serializable]
    public class AimTowardsDirection : BaseMovementPattern
    {
        [SerializeField] private Vector3 direction;

        public override void UpdatePosition()
        {
            targetTransform.position += direction * targetSpeedMultiplier * Time.deltaTime;
        }

        public void Initialize(Vector3 dir)
        {
            direction = dir.normalized;
        }
        
        public override object Clone()
        {
            var newObject = new AimTowardsDirection();
            newObject.Initialize(direction);
            return newObject;
        }
    }
}