using System.Collections.Generic;
using Helpers;

namespace Game.Extensions
{
    public static class ListExtension
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int randomIndex = RandomHelper.Random.Next(list.Count);
            return list[randomIndex];
        }
    }
}