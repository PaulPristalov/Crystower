using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private readonly List<InventoryItem> _items = new List<InventoryItem>();

    public Inventory()
    {
        Clear();
    }

    public List<T> GetItems<T>() where T : InventoryItem
    {
        List<T> buildings = new();
        buildings.AddRange(_items.Where(i => i is T).Select(i => i as T));
        return buildings;
    }

    public void AddItem(InventoryItem item)
    {
        if (item == null)
            throw new ArgumentNullException("The item is null!");

        _items.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        if (item == null || !_items.Contains(item))
            throw new ArgumentException("Can't remove the item " + item.Name);

        _items.Remove(item);
    }

    private void Clear()
    {
        _items.Clear();
    }
}
