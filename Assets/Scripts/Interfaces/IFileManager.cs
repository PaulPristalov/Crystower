using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IFileManager
    {
        void Save<T>(string FileName, T data) where T : class, new();
        void Load(string FileName, ScriptableObject obj);
    }
}
