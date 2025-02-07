using Assets.Scripts.Settings.constants;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Settings;
using Assets.Scripts.Utils;
using UnityEngine;


public class EntryBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenu.Inventory.UI _inventoryUI;
    [SerializeField] private MainMenu.Inventory.Item[] _startItems; // Test
    private MainMenu.Inventory.Vault _inventory;

    public bool Loaded { get; private set; } = false;

    private IFileManager fileManager;

    [SerializeField] private SettingsUI settingsUI;

    [Header("Vault of settings")]
    [SerializeField] private Assets.Scripts.Settings.Vault settingsVault;

    private void OnValidate()
    {
        settingsUI = FindObjectOfType<SettingsUI>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        fileManager = new FileManager();

        //Assets.Scripts.Settings.Vault vault1 = new()
        //{
        //    musicVolume = 0.1f,
        //    volume = 0.1f,
        //    isVibration = true,
        //    graphicsQuality = 1,
        //    language = Assets.Scripts.Settings.LanguageEnums.RUSSIA
        //};

        //fileManager.Save(FileNames.SETTINGS_NAME, vault1);

        settingsVault = fileManager.Load<Assets.Scripts.Settings.Vault>(FileNames.SETTINGS_NAME);
        print($"{settingsVault.musicVolume} \n {settingsVault.language} \n {settingsVault.volume}");

        settingsUI.Init(fileManager, settingsVault);

        // TODO: Get this from save file.
        MainMenu.Inventory.SelectedItem[] selectedItems = {
            new MainMenu.Inventory.SelectedItem(0, typeof(MainMenu.Inventory.BuildingItem)),
            new MainMenu.Inventory.SelectedItem(1, typeof(MainMenu.Inventory.BuildingItem)),
            new MainMenu.Inventory.SelectedItem(2, typeof(MainMenu.Inventory.BuildingItem)),
            new MainMenu.Inventory.SelectedItem(3, typeof(MainMenu.Inventory.BuildingItem)),
        };
        _inventory = new(_startItems, selectedItems);
        if (_inventoryUI)
            _inventoryUI.Initialize(_inventory);

        Loaded = true;
    }
}
