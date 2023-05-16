using System.Collections.Generic;
using Game.Weapons;
using Game.Weapons.Guns;

namespace Game.StatsCalculators
{
    public interface ICooldownCalculator
    {
        public float GetTotalCooldown(WeaponStats weaponBaseStats, IEnumerable<WeaponUpgrade> upgrades);
    }
}