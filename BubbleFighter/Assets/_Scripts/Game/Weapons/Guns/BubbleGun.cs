using System.Collections.Generic;
using Game.MovementPatterns;
using UnityEngine;
using Utilities;

namespace Game.Weapons.Guns
{
    public class BubbleGun : BaseProjectileWeapon
    {
        protected override IMovementPattern GetMovementPattern()
        {
            return new MovementPatternAwayFromPlayer();
            //return new MovementPatternAimTowardsMouse();
            //return new MovementPatternSpiralFromInitialPosition();
            //return new MovementPatternCircleAroundPlayer(5f,100f);
            //return new MovementPatternCircleAroundPoint(10f,5f,Vector2.zero);
            //return new MovementPatternSpiralAroundPoint(5000f,5f,transform.position + Vector3.left);
            /*
            return new MovementPatternSpiralAroundPoint(new SpiralMovementData()
            {
                timeScale = 1f,
                angularVelocity = 5f,
                initialRadius = new Vector2(1f,1f),
                linearVelocity = new Vector2(0,0),
                radiusExpansionMultiplier = new Vector2(1,1),
                constantSpeed = true
            });
            */
        }

        protected override List<Vector3> GetProjectileSpawnPointsOffsets()
        {
            return GeometryHelper.GenerateCircularSpawnOffsets(8,0.5f,15);
        }
    }
}