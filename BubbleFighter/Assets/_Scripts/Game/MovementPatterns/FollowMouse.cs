using System;
using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    [Serializable]
    public class FollowMouse : BaseMovementPattern
    {
        public override void UpdatePosition()
        {
            Vector3 mousePos = MouseUtils.GetMouseWorldPosition;
            var position = targetTransform.position;
            
            Vector3 direction = (mousePos - position).normalized;

            targetTransform.Translate(direction * targetSpeedMultiplier * Time.deltaTime);
        }

        public override object Clone()
        {
            return new FollowMouse();
        }
    }
}