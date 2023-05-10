using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.SpawnPoints
{
    [Serializable]
    public class SquareSpawnPointProvider : IProjectileSpawnPointProvider
    {
        public List<Vector3> GetSpawnPointsOffsets()
        {
            return new List<Vector3>()
            {
                new Vector3(1f, 1f, 0f),
                new Vector3(1f, -1f, 0f),
                new Vector3(-1f, 1f, 0f),
                new Vector3(-1f, -1f, 0f)
            };
        }
    }
}