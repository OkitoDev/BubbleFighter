using System;
using UnityEngine;

namespace Game.MovementPatterns
{
    public interface IMovementPattern : ICloneable
    {
        public void UpdatePosition();
        public void SetValues(Transform transform, float speedMultiplier);
    }
}