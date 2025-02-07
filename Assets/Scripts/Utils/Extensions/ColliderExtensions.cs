using UnityEngine;

namespace FirerusUtilities.Extensions
{
    public static class ColliderExtensions
    {
        public static Collider2D GetNearest(this Collider2D[] array, Vector3 position)
        {
            if (array.Length == 0)
            {
                return null;
            }

            Collider2D nearest = array[0];

            if (array.Length > 1)
            {
                for (int i = 1; i < array.Length; i++)
                {
                    if (Vector3.Distance(position, nearest.transform.position) >
                        Vector3.Distance(position, array[i].transform.position))
                    {
                        nearest = array[i];
                    }
                }
            }

            return nearest;
        }
    }
}
