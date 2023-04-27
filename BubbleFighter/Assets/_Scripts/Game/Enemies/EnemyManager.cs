using System;
using System.Collections.Generic;
using Enums;
using Helpers;
using Interfaces;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        private readonly List<IEnemy> _enemies = new List<IEnemy>();

        public void SpawnEnemy(BaseEnemy enemyPrefab, EnemyType enemyType, Vector3 spawnPlace)
        {
            _enemies.Add(Instantiate(enemyPrefab, transform).Init(enemyType, spawnPlace));
        }

        public IEnemy GetClosestEnemy(Vector3 position)
        {
            IEnemy nearestEnemy = null;
            float closestDistanceSqr = Mathf.Infinity;

            foreach(IEnemy enemy in _enemies)
            {
                var enemyTransform = enemy.Transform;
                Vector3 directionToTarget = enemyTransform.position - position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget > closestDistanceSqr) continue;
                
                closestDistanceSqr = dSqrToTarget;
                nearestEnemy = enemy;
            }
     
            return nearestEnemy;
        }
    }
}