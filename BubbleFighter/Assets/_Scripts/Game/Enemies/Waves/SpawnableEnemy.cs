namespace Game.Enemies.Waves
{
    [System.Serializable]
    public struct SpawnableEnemy
    {
        public BaseEnemy enemy;
        public float spawnProbability;
    }
}