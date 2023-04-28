using UnityEngine;

namespace Helpers
{
    public static class RandomHelper
    {

        public static Vector2 GetRandomPositionFromVector(Vector2 originalVector, int minDistance, int maxDistance)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(0f, 2f * Mathf.PI);
            Vector2 randomDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 randomPosition = originalVector + distance * randomDirection;
            return randomPosition;
        }
    }
}