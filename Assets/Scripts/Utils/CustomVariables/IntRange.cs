using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FirerusUtilities
{
    [Serializable]
    public struct IntRange
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        public int Min => _min;
        public int Max => _max;

        public IntRange(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public IntRange(int value)
        {
            _min = _max = value;
        }

        public int GetRandomValue()
        {
            return Random.Range(_min, _max);
        }
    }
}
