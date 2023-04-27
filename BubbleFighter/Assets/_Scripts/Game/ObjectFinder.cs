using Game.Enemies;
using UnityEngine;

namespace Game
{
    public static class ObjectFinder
    {
        public static Player Player => Object.FindObjectOfType<Player>();
        public static Camera Camera => Object.FindObjectOfType<Camera>();
        private static EnemyManager _enemyManager;
        public static EnemyManager EnemyManager => _enemyManager ??= Object.FindObjectOfType<EnemyManager>();
    }
}