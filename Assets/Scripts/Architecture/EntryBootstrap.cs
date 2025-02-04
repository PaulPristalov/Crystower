using Assets.Scripts.Interfaces;
using Assets.Scripts.Settings.constants;
using Assets.Scripts.Utils;
using UnityEngine;


public class EntryBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenu.Inventory.UI _inventoryUI;
    [SerializeField] private MainMenu.Inventory.Item[] _startItems; // Test
    private MainMenu.Inventory.Vault _inventory;

    private IFileManager fileManager;

    [Header("Vault of settings")]
    [SerializeField] private Assets.Scripts.Settings.Vault settingsVault;
    public bool Loaded { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        fileManager = new FileManager();

        //Assets.Scripts.Settings.Vault vault1 = new()
        //{
        //    musicVolume = 1f,
        //    volume = 1f,
        //    isVibration = true,
        //    graphicsQuality = 1,
        //    language = Assets.Scripts.Settings.LanguageEnums.RUSSIA
        //};

        //fileManager.Save(FileNames.SETTINGS_NAME, vault1);

        fileManager.Load(FileNames.SETTINGS_NAME, settingsVault);

        // TODO: Get this from save file.
        MainMenu.Inventory.SelectedItem[] selectedItems = {
            new MainMenu.Inventory.SelectedItem(0, typeof(MainMenu.Inventory.BuildingItem)),
            new MainMenu.Inventory.SelectedItem(1, typeof(MainMenu.Inventory.BuildingItem)),
            new MainMenu.Inventory.SelectedItem(2, typeof(MainMenu.Inventory.BuildingItem)),
            new MainMenu.Inventory.SelectedItem(3, typeof(MainMenu.Inventory.BuildingItem)),
        };
        _inventory = new(_startItems, selectedItems);
        _inventoryUI.Initialize(_inventory);

        Loaded = true;
    }
}
