using Assets.Scripts.Interfaces;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    internal class FileManager : IFileManager
    {
        public void Load(string FileName, ScriptableObject obj)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
            {
                string json = reader.ReadString();
                JsonUtility.FromJsonOverwrite(json, obj);

                Debug.Log(json);
            }
        }

        public void Save<T>(string FileName, T data) where T : class, new()
        {
            using (BinaryWriter writer = new(File.Open(FileName, FileMode.Create)))
            {
                string json = JsonUtility.ToJson(data);
                writer.Write(json);

                Debug.Log($"{json} is saved");
            }
        }
    }
}
