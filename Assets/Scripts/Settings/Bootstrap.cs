using Assets.Scripts.Settings.constants;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private IFileManager fileManager;

    private void Awake()
    {
        fileManager = new FileManager();

        Assets.Scripts.Settings.Vault vault1 = fileManager.Load<Assets.Scripts.Settings.Vault>(FileNames.SETTINGS_NAME);
        print(JsonUtility.ToJson(vault1));
    }
}
