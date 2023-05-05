using System.Collections.Generic;
using System.Linq;
using Enums;
using Extensions;
using Game.Enemies.Waves;
using UnityEngine;
using Utilities;

namespace Game.Enemies
{
    // Those two should not depend on each other like that
    [RequireComponent(typeof(EnemySpawner))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveData> waves;
        [SerializeField] private int timePerWave = 30;
        private WaveData _currentWave;
        private Transform _player;
        private int _currentWaveIndex = 0;
        private float _initialDelay = 1f;
        private bool LastWaveWasReached => _currentWaveIndex >= waves.Count - 1;
        private EnemyManager _enemyManager;

        private void Awake()
        {
            _enemyManager = GetComponent<EnemyManager>();
            _player = Services.GetServiceFromScene<Player.Player>().transform;
            _currentWave = waves.FirstOrDefault();
            Invoke(nameof(StartNextWave),timePerWave);
            RestartSpawning();
        }

        public void StartNextWave()
        {
            Invoke(nameof(StartNextWave),timePerWave);
            IncreaseEnemyStats();
            RestartSpawning();
            if (LastWaveWasReached || waves == null || waves.Count == 0) return;

            _currentWaveIndex++;
            _currentWave = waves[_currentWaveIndex];
        }

        private void IncreaseEnemyStats()
        {
            GlobalValues.AddEnemyDamageMultiplier(100f);
            GlobalValues.AddEnemyHealthMultiplier(100f);
            GlobalValues.AddGlobalEnemyWorth(100f);
        }

        private void RestartSpawning()
        {
            CancelInvoke(nameof(StartSpawningEnemies));
            InvokeRepeating(nameof(StartSpawningEnemies),_initialDelay,1/_currentWave.spawnRate);
        }

        private void StartSpawningEnemies()
        {
            _enemyManager.SpawnEnemy(_currentWave.spawnableEnemies.GetRandomElement().enemy,GetRandomEnemyType(),GetRandomSpawnPoint());
        }

        private Vector3 GetRandomSpawnPoint()
        {
            return RandomUtils.GetRandomPositionFromVector((Vector2) _player.transform.position, 10, 20);
        }

        private EnemyType GetRandomEnemyType()
        {
            float rand = Random.value;
            if (rand < 0.8f) return EnemyType.Normal;
            if (rand < 0.88f) return EnemyType.Tank;
            if (rand < 0.96f) return EnemyType.Assassin; 
            
            return EnemyType.MiniBoss;
        }
    }   
}