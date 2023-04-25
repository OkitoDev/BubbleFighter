using UnityEngine;

namespace Game
{
    public class BulletSpawner
    {
        public void SpawnBasicBullet(Transform firePoint, Color bulletColor, float bulletSpeed, float size, float damage)
        {
            var bulletPrefab = GameAssets.Instance.prefabBasicBullet;
            var spawnedBullet = Object.Instantiate(bulletPrefab, firePoint.position,firePoint.rotation).GetComponent<Bullet>();
            spawnedBullet.ChangeColor(bulletColor);
            spawnedBullet.SetBulletSpeed(firePoint.up, bulletSpeed);
            spawnedBullet.SetSize(size);
            spawnedBullet.SetDamage(damage);
        }
    }
}