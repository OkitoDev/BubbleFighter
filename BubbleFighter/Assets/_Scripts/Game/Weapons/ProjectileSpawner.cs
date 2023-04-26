using UnityEngine;

namespace Game.Weapons
{
    public class ProjectileSpawner
    {
        
        
        public ProjectileSpawner()
        {
            
        }
        
        public void SpawnBubbleProjectile(Transform firePoint, Vector2 direction, Color projectileColor, float projectileSpeed, float size, float damage, bool isPlayer)
        {
            var projectile = GameAssets.Instance.prefabBubbleProjectile;
            Projectile spawnedProjectile = Object.Instantiate(projectile, firePoint.position, firePoint.rotation);
            spawnedProjectile.ChangeColor(projectileColor).SetProjectileSpeed(direction, projectileSpeed).
                SetSize(size).SetDamage(damage).SetCreator(isPlayer);
        }
    }
}