using UnityEngine;

namespace Assets.Scripts.Settings
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
    internal class Vault : ScriptableObject
    {
        public float musicVolume;
        public float volume;
        public bool isVibration;
        public int graphicsQuality;
        public LanguageEnums language;
    }
}
