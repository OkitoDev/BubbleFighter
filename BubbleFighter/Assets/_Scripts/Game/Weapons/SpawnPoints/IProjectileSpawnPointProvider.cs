using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons.SpawnPoints
{
    public interface IProjectileSpawnPointProvider
    {
        public List<Vector3> GetSpawnPointsOffsets();
    }
}