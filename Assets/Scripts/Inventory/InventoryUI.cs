using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
        UpdateUI();
    }

    public void UpdateUI()
    {
        Debug.Log(1);
    }
}
