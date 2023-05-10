using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.SpawnPoints
{
    [Serializable]
    public class CircularSpawnPointProvider : IProjectileSpawnPointProvider
    {
        [SerializeField] [Range(1, 20)] private int spawnPointCount = 4;
        [SerializeField] [Range(0, 360f)] private float initialRotation = 0f;
        [SerializeField] [Range(0, 10f)] private float radius = 1f;
        
        public List<Vector3> GetSpawnPointsOffsets()
        {
            var offsets = new List<Vector3>();
            float angleIncrement = 2 * Mathf.PI / spawnPointCount;

            for (var i = 0; i < spawnPointCount; i++)
            {
                float angle = i * angleIncrement + initialRotation * Mathf.Deg2Rad + Mathf.PI / 2;
                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);
                offsets.Add(new Vector2(x, y));
            }
            
            return offsets;
        }

        public void SetValues(int count, float rotation, float circleRadius)
        {
            spawnPointCount = count;
            initialRotation = rotation;
            radius = circleRadius;
        }
    }
}