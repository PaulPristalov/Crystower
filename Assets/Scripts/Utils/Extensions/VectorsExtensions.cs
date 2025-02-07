using UnityEngine;

namespace FirerusUtilities.Extensions
{
    public static class VectorsExtensions
    {
        public static Vector3 AngleToVector(this float angle) => new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)).normalized;

        public static float RotateTo(this Vector3 start, Vector3 target)
        {
            Vector3 difference = target - start;
            float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            return rotation;
        }
    }
}
