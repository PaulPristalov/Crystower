using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FirerusUtilities.Extensions
{
    public static class CollectionExtensions
    {
        public static T GetRandomElement<T>(this T[] array) => array[Random.Range(0, array.Length)];

        public static T GetRandomElement<T>(this List<T> list) => list[Random.Range(0, list.Count)];

        public static TComponent[] GetComponent<T, TComponent>(this T[] array) 
            where T : Component 
            where TComponent : Component
        {
            List<TComponent> components = new List<TComponent>();
            for (int i = 0; i < array.Length; i++)
            {
                try
                {
                    components.Add(array[i].GetComponent<TComponent>());
                }
                catch
                {
                    continue;
                }
            }

            return components.ToArray();
        }

        public static int CountOfElements<T>(this IEnumerable<T> array, T element)
        {
            int result = 0;
            foreach (var el in array)
            {
                if (el.Equals(element))
                {
                    result++;
                }
            }
            return result;
        }

        public static void Shuffle<T>(this T[] array)
        {
            for (int i = array.Length - 1; i >= 1; i--)
            {
                int j = Random.Range(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        public static void Shuffle<T>(this List<T> list) => list.ToArray().Shuffle();

        public static T[] GetShuffledCopy<T>(this T[] array)
        {
            T[] shuffledArray = new T[array.Length];
            for (int i = array.Length - 1; i >= 1; i--)
            {
                int j = Random.Range(0, i + 1);
                shuffledArray[i] = array[j];
            }
            return shuffledArray;
        }

        public static List<T> GetShuffledCopy<T>(this List<T> list) => list.ToArray().GetShuffledCopy().ToList();
    }
}
