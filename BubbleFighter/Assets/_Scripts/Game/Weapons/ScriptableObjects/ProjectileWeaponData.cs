using System;
using Game.MovementPatterns;
using Game.Projectiles;
using Game.StatsCalculators;
using Game.Upgrades.UpgradeTrees;
using Game.Weapons.Guns;
using Game.Weapons.SpawnPoints;
using UnityEngine;

namespace Game.Weapons.ScriptableObjects
{
    [Serializable]
    [CreateAssetMenu(menuName = "Weapons/ProjectileWeapons", fileName = "Weapon")]
    public class ProjectileWeaponData : ScriptableObject
    {
#if UNITY_EDITOR
        public event Action<ProjectileWeaponData> OnScriptableObjectChange;
#endif
        [SerializeReference, SubclassSelector] public IMovementPattern projectileMovementPattern;
        [SerializeReference, SubclassSelector] public IProjectileSpawnPointProvider projectileSpawnPointProvider;
        [SerializeReference, SubclassSelector] public IWeaponStatsCalculator weaponStatsCalculator;
        public BaseProjectile projectilePrefab;
        public WeaponStats weaponBaseStats;
        public Sprite projectileSprite;
        
        [Header("Possible upgrades")]
        public BaseDamageUpgradeTree baseDamageUpgradeTree;
        public DamageMultiplierUpgradeTree damageMultiplierUpgradeTree;
        public ProjectileMovementUpgradeTree projectileMovementUpgradeTree;
        public ProjectileSpawnPointsUpgradeTree projectileSpawnPointsUpgradeTree;

        public ProjectileWeaponData(IMovementPattern projectileMovementPattern, IProjectileSpawnPointProvider projectileSpawnPointProvider, IWeaponStatsCalculator weaponStatsCalculator)
        {
            this.projectileMovementPattern = projectileMovementPattern;
            this.projectileSpawnPointProvider = projectileSpawnPointProvider;
            this.weaponStatsCalculator = weaponStatsCalculator;
        }

        public void OnValidate() 
        {
            OnScriptableObjectChange?.Invoke(this);
        }
    }
}