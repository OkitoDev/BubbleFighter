using UnityEngine;

namespace Game.MovementPatterns
{
    /// <summary>
    /// That's a bit hacky but Idc
    /// </summary>
    public abstract class MovementPatternSetter
    {
        protected Transform targetTransform;
        protected float targetSpeedMultiplier;

        public virtual void SetValues(Transform transform, float speedMultiplier)
        {
            targetTransform = transform;
            targetSpeedMultiplier = speedMultiplier;
        }
    }
}