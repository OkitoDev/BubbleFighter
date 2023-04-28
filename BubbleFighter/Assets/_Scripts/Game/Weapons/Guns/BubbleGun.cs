using Game.MovementPatterns;

namespace Game.Weapons.Guns
{
    public class BubbleGun : BaseGunWeapon
    {
        protected override IMovementPattern GetMovementPattern()
        {
            return new MovementPatternAimTowardsMouse();
        }
    }
}