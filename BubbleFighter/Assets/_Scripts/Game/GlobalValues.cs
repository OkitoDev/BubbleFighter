namespace Game
{
    public static class GlobalValues
    {
        // TODO that's only temporary
        // Damage works in percentages
        // (e.g. damage multiplier +100% buff would bring it from 1 => 2)
        public static float GlobalDamage = 1f;
        public static float GlobalDamageMultiplier = 1f;
        public static float GlobalCooldown = 1f;
        public static float GlobalBulletSize = 1f;
        public static float GlobalBulletSpeed = 1f;

        public static void AddGlobalDamage(float damageToAdd)
        {
            GlobalDamage += damageToAdd;
        }
        
        public static void AddGlobalDamageMultiplier(float damageMultiplierToAdd)
        {
            GlobalDamageMultiplier += damageMultiplierToAdd;
        }
        
        public static void AddGlobalCooldown(float cooldownToAdd)
        {
            GlobalCooldown += cooldownToAdd;
        }
        
        public static void AddGlobalBulletSize(float bulletSizeToAdd)
        {
            GlobalBulletSize += bulletSizeToAdd;
        }

        public static void AddGlobalBulletSpeed(float bulletSpeedToAdd)
        {
            GlobalBulletSpeed += bulletSpeedToAdd;
        }
    }
}