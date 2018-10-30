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
            var count = list.Count;
            var random = new Random();

            while (count > 1)
            {
                count--;
                var index = random.Next(count + 1);
                var value = list[index];
                list[index] = list[count];
                list[count] = value;
            }

            return list;
        }

        /// <summary>
        /// Get a random item from list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(this List<T> list)
        {
            var random = new Random();
            var next = random.Next(list.Count);
            return list[next];
        }

        /// <summary>
        /// Get a random enum member
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Random<T>(this T @enum)
        {
            var random = new Random();
            var values = (T[]) Enum.GetValues(typeof(T));
            return values[random.Next(values.Length)];
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