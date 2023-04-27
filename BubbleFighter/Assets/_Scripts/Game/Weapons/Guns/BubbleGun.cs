using Game.Weapons.Projectiles;
using Game.Weapons.Projectiles.Patterns;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public class BubbleGun : BaseGunWeapon
    {
        protected override IProjectilePattern GetProjectilePattern()
        {
            return new ProjectilePatternAimTowardsMouse();
        }
    }
}