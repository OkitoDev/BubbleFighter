using System.Collections.Generic;
using Helpers;

namespace Extensions
{
    public static class ListExtensions
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int randomIndex = RandomHelper.Random.Next(list.Count);
            return list[randomIndex];
        }
    }
}