using System;
using UnityEngine;

namespace Game.MovementPatterns
{
    public abstract class BaseMovementPattern : IMovementPattern
    {
        protected Transform targetTransform;
        protected float targetSpeedMultiplier;

        public virtual void SetValues(Transform transform, float speedMultiplier)
        {
            targetTransform = transform;
            targetSpeedMultiplier = speedMultiplier;
        }

        public abstract void UpdatePosition();

        public abstract object Clone();
    }
}