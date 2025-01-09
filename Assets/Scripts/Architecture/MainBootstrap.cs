using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    [SerializeField] private InventoryUI _ui;
    private Inventory _inventory;

    public bool Loaded { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _inventory = new Inventory();
        _ui.Initialize(_inventory);

        Loaded = true;
    }
}
