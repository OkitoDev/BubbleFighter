using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class GeometryHelper
    {
        /// <summary>
        /// Generates offsets for spawn points arranged in a circular pattern,
        /// starting from an initial position and moving around the circle in a clockwise direction.
        /// (starts from the top-side of the circle)
        /// </summary>
        /// <param name="spawnPointCount">Number of spawn points to generate</param>
        /// <param name="radius">Radius of the circle</param>
        /// <param name="initialRotation">Initial rotation of the first point</param>
        /// <returns></returns>
        public static List<Vector3> GenerateSpawnPointOffsetsOnACircle(int spawnPointCount, float radius, float initialRotation = 0) 
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
        
    }
}