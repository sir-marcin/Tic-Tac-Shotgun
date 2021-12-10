using System;
using System.Collections.Generic;

namespace TicTacShotgun.Utils
{
    public static class ListExtensions
    {
        static Random Random;
        
        public static T GetRandomElement<T>(this List<T> list)
        {
            if (Random == null)
            {
                Random = new Random();
            }

            return list[Random.Next(0, list.Count)];
        }
    }
}