using System.Collections.Generic;
using Game.Weapons;
using Game.Weapons.Guns;

namespace Game.StatsCalculators
{
    public interface ITotalDamageCalculator
    {
        public float GetTotalDamage(WeaponStats weaponBaseStats, List<WeaponUpgrade> upgrades);
    }
}