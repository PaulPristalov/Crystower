using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MainMenu.Inventory
{
    public class Vault
    {
        private readonly List<Item> _items = new();
        private readonly List<SelectedItem> _selectedItems = new();

        public event Action OnChanged;

        public Vault(Item[] items, SelectedItem[] selectedItems)
        {
            Clear();
            _items.AddRange(items);
            _selectedItems.AddRange(selectedItems);
            UnityEngine.Debug.Log($"i{_items.Count}, s{_selectedItems.Count}");
        }

        public List<T> GetItems<T>() where T : Item
        {
            // TODO: To utilities method
            List<T> buildings = new();
            buildings.AddRange(_items.Where(i => i is T).Select(i => i as T));
            return buildings;
        }

        public void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("The item is null!");

            //if (!_items.Contains(item))
            //    _items.Add(item);
            //else
            //    _items.Find(i => i == item).Count++;
            
            _items.Add(item);
            OnChanged?.Invoke();
        }

        public void RemoveItem(Item item)
        {
            if (item == null || !_items.Contains(item))
                throw new ArgumentException("Can't remove the item " + item.Name);

            //if (_items.Find(i => item).Count > 1)
            //    _items.Find(i => item).Count--;
            //else
            //    _items.Remove(item);

            _items.Remove(item);
            OnChanged?.Invoke();
        }

        public List<T> GetSelectedItems<T>() where T : Item
        {
            List<T> items = new();
            for (int i = 0; i < _selectedItems.Count; i++)
            {
                if (_selectedItems[i].Type == typeof(T))
                {
                    items.Add(_items[_selectedItems[i].Index] as T);
                }
            }
            return items;
        }

        public void SelectItem(Item item, int index)
        {
            //_selectedItems[index] = item;
            OnChanged?.Invoke();
        }

        private void Clear()
        {
            _items.Clear();
        }
    }
}

