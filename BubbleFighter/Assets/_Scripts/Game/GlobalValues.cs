using UnityEngine;

namespace Game
{
    public static class GlobalValues
    {
        // TODO that's only temporary
        // Damage works in percentages
        // (e.g. damage multiplier +100% buff would bring it from 1 => 2)
        public static float PlayerGold { get; private set; } = 1f;
        public static float PlayerHealth{ get; private set; } = 100f;
        public static float PlayerMaxHealth{ get; private set; } = 100f;
        public static float GlobalEnemyHealthMultiplier { get; private set; } = 1f;
        public static float GlobalEnemyHealth { get; private set; } = 0f;
        public static float GlobalEnemyMovementSpeed { get; private set; } = 1f;
        public static float GlobalEnemyDamage { get; private set; } = 0f;
        public static float GlobalEnemyWorth { get; private set; } = 1f;
        public static float GlobalEnemyDamageMultiplier { get; private set; } = 1f;
        public static float GlobalEnemyCooldownReduction { get; private set; } = 1f;
        
        public static float GlobalPlayerDamage { get; private set; } =  0f;
        public static float GlobalPlayerDamageMultiplier { get; private set; } = 1f;
        public static float GlobalPlayerCooldownReduction { get; private set; } = 1f;
        public static float GlobalProjectileSize { get; private set; } = 1f;
        public static float GlobalProjectileSpeed { get; private set; } = 1f;

        public static void AddDamage(float damageToAdd)
        {
            GlobalPlayerDamage += damageToAdd;
        }

        public static void AddDamageMultiplier(float damageMultiplierToAdd)
        {
            GlobalPlayerDamageMultiplier += damageMultiplierToAdd / 100f;
        }
        
        public static void DecreaseCooldown(float cooldownDecrease)
        {
            GlobalPlayerCooldownReduction = ((100f - cooldownDecrease) / 100f) * GlobalPlayerCooldownReduction;
        }
        
        public static void AddProjectileSize(float projectileSizeToAdd)
        {
            GlobalProjectileSize += projectileSizeToAdd / 100f;
        }

        public static void AddProjectileSpeed(float projectileSpeedToAdd)
        {
            GlobalProjectileSpeed += projectileSpeedToAdd / 100f;
        }

        public static void AddPlayerGold(float goldToAdd)
        {
            PlayerGold += goldToAdd;
            Debug.Log(PlayerGold);
        }

        public static void AddEnemyHealthMultiplier(float enemyHealthMultiplierToAdd)
        {
            GlobalEnemyHealthMultiplier += enemyHealthMultiplierToAdd / 100f;
        }
        
        public static void AddEnemyHealth(float enemyHealthToAdd)
        {
            GlobalEnemyHealth += enemyHealthToAdd;
        }

        public static void AddEnemyMovementSpeed(float movementSpeedToAdd)
        {
            GlobalEnemyMovementSpeed += movementSpeedToAdd / 100f;
        }
        
        public static void AddEnemyDamage(float enemyDamageToAdd)
        {
            GlobalPlayerDamage += enemyDamageToAdd;
        }
        
        public static void AddEnemyDamageMultiplier(float enemyDamageMultiplierToAdd)
        {
            GlobalEnemyDamageMultiplier += enemyDamageMultiplierToAdd / 100f;
        }

        public static void DecreaseEnemyAttackCooldown(float cooldownDecrease)
        {
            GlobalEnemyCooldownReduction = ((100f - cooldownDecrease) / 100f) * GlobalEnemyCooldownReduction;
        }

        public static void AddGlobalEnemyWorth(float enemyWorthToAdd)
        {
            GlobalEnemyWorth += enemyWorthToAdd / 100f;
        }

        public static void AddPlayerMaxHealth(float healthToAdd)
        {
            PlayerMaxHealth -= healthToAdd;
        }

        public static void HealPlayer(float healAmount)
        {
            PlayerHealth = Mathf.Min(PlayerHealth + healAmount,PlayerMaxHealth);
        }

        public static void DamagePlayer(float damageAmount)
        {
            PlayerHealth -= damageAmount;
            Debug.Log(PlayerHealth);
        }
    }
}