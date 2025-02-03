using UnityEngine;


public class EntryBootstrap : MonoBehaviour
{
    [SerializeField] private MainMenu.Inventory.UI _inventoryUI;
    [SerializeField] private MainMenu.Inventory.Item[] _startItems; // Test
    private MainMenu.Inventory.Vault _inventory;

    public bool Loaded { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);

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
