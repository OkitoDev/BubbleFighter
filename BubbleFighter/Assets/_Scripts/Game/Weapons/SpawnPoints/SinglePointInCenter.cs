using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.SpawnPoints
{
    [Serializable]
    public class SinglePointInCenter : IProjectileSpawnPointProvider
    {
        public List<Vector3> GetSpawnPointsOffsets()
        {
            return new List<Vector3>()
            {
                Vector3.zero
            };
        }
    }
}