using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class PositionHelper
    {
        /// <summary>
        /// Generates offsets for spawn points arranged in a circular pattern,
        /// starting from an initial position and moving around the circle in a clockwise direction.
        /// </summary>
        /// <param name="spawnPointCount">Number of spawn points to generate</param>
        /// <param name="radius">Radius of the circle</param>
        /// <param name="initialRotation">Initial rotation of the first point</param>
        /// <returns></returns>
        public static List<Vector3> GenerateCircularSpawnOffsets(int spawnPointCount, float radius = 1f, float initialRotation = 0)
        {
            var offsets = new List<Vector3>();
            float angleIncrement = 2 * Mathf.PI / spawnPointCount;

            for (var i = 0; i < spawnPointCount; i++)
            {
                float angle = i * angleIncrement;
                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);
                offsets.Add(new Vector2(x, y));
            }

            return offsets;
        }
    }
}