using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class Services
    {
        public static T GetServiceFromComponent<T>() where T : Component
        {
            var instanceList = Object.FindObjectsOfType<T>();
            
            if (instanceList.Length == 0)
            {
                Debug.LogWarning($"Instance not found! Initializing a default {nameof(T)}");
                return Object.Instantiate(new GameObject()).AddComponent<T>();
            }

            if (instanceList.Length > 1)
            {
                Debug.LogWarning($"Found more than one instance of {nameof(T)}! Returning the first one in list");
            }

            return instanceList.FirstOrDefault();
        }
    }
}