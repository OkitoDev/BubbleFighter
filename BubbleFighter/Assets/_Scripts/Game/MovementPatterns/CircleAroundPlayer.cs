using System;
using UnityEngine;
using Utilities;

namespace Game.MovementPatterns
{
    [Serializable]
    public class CircleAroundPlayer : BaseMovementPattern
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float radius;
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 offset;

        public override void SetValues(Transform transform, float speedMultiplier)
        {
            base.SetValues(transform, speedMultiplier);
            player = Services.GetServiceFromComponent<Player.Player>().transform;
            offset = targetTransform.position - player.position;
        }

        public override void UpdatePosition()
        {
            var playerPosition = player.position;
            targetTransform.position = playerPosition + offset;
            
            targetTransform.RotateAround(playerPosition, Vector3.forward, rotationSpeed * Time.deltaTime);
            offset = targetTransform.position - playerPosition;
            
            targetTransform.position += offset.normalized * radius;
        }

        public void Initialize(float circleRadius, float rotateSpeed)
        {
            radius = circleRadius;
            rotationSpeed = rotateSpeed;
        }

        public override object Clone()
        {
            var newObject = new CircleAroundPlayer();
            newObject.Initialize(radius, rotationSpeed);
            return newObject;
        }
    }
}