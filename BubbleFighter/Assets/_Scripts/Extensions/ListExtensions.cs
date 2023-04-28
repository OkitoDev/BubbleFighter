using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}