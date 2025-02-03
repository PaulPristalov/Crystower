using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

namespace MainMenu.Inventory
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private ItemVisual _itemPrefab;
        [SerializeField] private RectTransform _itemsParent;
        [SerializeField] private RectTransform _selectedItemsParent;

        private Vault _vault;
        private readonly List<ItemVisual> _items = new();

        public void Initialize(Vault vault)
        {
            for (int i = 0; i < _itemsParent.childCount; i++)
            {
                Destroy(_itemsParent.GetChild(i).gameObject);
            }
            for (int i = 0; i < _selectedItemsParent.childCount; i++)
            {
                Destroy(_selectedItemsParent.GetChild(i).gameObject);
            }

            _vault = vault;
            _vault.OnChanged += UpdateUI;
            UpdateUI();
        }

        public void OnDestroy()
        {
            _vault.OnChanged -= UpdateUI;
        }

        public void UpdateUI()
        {
            ClearItems();
            var items = _vault.GetItems<BuildingItem>();
            foreach (var item in items)
            {
                ItemVisual instance = _items.Find(i => i.Item == item);
                if (instance == null)
                {
                    var i = Instantiate(_itemPrefab, _itemsParent);
                    i.Initialize(item, 1);
                    _items.Add(i);
                }
                else
                    instance.Count++;
            }

            var selectedItems = _vault.GetSelectedItems<BuildingItem>();
            foreach (var item in selectedItems)
            {
                ItemVisual instance = _items.Find(i => i.Item == item);
                instance.transform.SetParent(_selectedItemsParent);
            }
        }

        public void SwitchType(int index)
        {
            Debug.Log($"Type: {index}");
        }

        public void SwitchSetup(int index)
        {
            Debug.Log($"Setup: {index}");
        }

        private void ClearItems()
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();
        }
    }
}
