using System;
using System.Collections.Generic;
using System.Linq;

namespace MemoryGame_1B
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Shuffles the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static List<T> Shuffle<T>(this List<T> list)
        {
            var list1 = list.ToList();
            var count = list.Count;
            var random = new Random();

            while (count > 1)
            {
                count--;
                var index = random.Next(count + 1);
                var value = list1[index];
                list1[index] = list1[count];
                list1[count] = value;
            }

            return list1;
        }

        /// <summary>
        /// Removes and returns the first item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Pop<T>(this List<T> list)
        {
            var first = list.First();
            list.RemoveAt(0);
            return first;
        }
    }
}