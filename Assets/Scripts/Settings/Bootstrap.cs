using Assets.Scripts.Settings.constants;
using Assets.Scripts.Settings;
using Assets.Scripts.Utils;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private FileManager fileManager;

    private void Awake()
    {
        fileManager = new FileManager();

        Assets.Scripts.Settings.Vault vault1 = fileManager.Load<Assets.Scripts.Settings.Vault>(FileNames.SETTINGS_NAME);
        print(JsonUtility.ToJson(vault1));
    }
}
