using Game.MovementPatterns;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public class BubbleGun : BaseProjectileWeapon
    {
        protected override IMovementPattern GetMovementPattern()
        {
            //return new MovementPatternAimTowardsMouse();
            //return new MovementPatternSpiralFromInitialPosition();
            //return new MovementPatternCircleAroundPlayer(5f,100f);
            //return new MovementPatternCircleAroundPoint(10f,5f,Vector2.zero);
            return new MovementPatternSpiralAroundPoint(5000f,5f,transform.position + Vector3.left);
        }
    }
}