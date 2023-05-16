using System.Collections.Generic;
using Game.StatsCalculators;
using UnityEngine;

namespace Game.Weapons.Guns
{
    public abstract class WeaponBase
    {
        private readonly List<WeaponUpgrade> _upgrades = new List<WeaponUpgrade>();
        private readonly MonoBehaviour _weaponHolder;
        private readonly Transform _baseFirePoint;
        private IProjectileWeaponStatsCalculator _projectileWeaponStatsCalculator;
    }
}