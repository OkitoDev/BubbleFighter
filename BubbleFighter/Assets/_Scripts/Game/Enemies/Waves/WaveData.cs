using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies.Waves
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Waves/Basic Wave")]
    public class WaveData : ScriptableObject
    {
        public List<SpawnableEnemy> spawnableEnemies;
        public int maxEnemies;
        public float spawnRate;
    }
}