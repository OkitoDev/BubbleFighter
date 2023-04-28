using UnityEngine;

namespace Game.MovementPatterns
{
    public interface IMovementPattern
    {
        public void UpdatePosition();
        public void SetValues(Transform transform, float speedMultiplier);
    }
}