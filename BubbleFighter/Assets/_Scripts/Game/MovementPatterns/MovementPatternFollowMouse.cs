using UnityEngine;

namespace Game.MovementPatterns
{
    public class MovementPatternFollowMouse : MovementPatternSetter, IMovementPattern
    {

        public void UpdatePosition()
        {
            Vector3 mousePos = Mouse.GetMouseWorldPosition;
            var position = targetTransform.position;
            
            Vector3 direction = (mousePos - position).normalized;

            targetTransform.Translate(direction * targetSpeedMultiplier * Time.deltaTime);
        }
    }
}