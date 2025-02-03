using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Inventory
{
    public abstract class ItemVisual : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Image _icon;

        private int _count;

        public Item Item { get; private set; }
        public int Count
        { 
            get => _count;
            set
            {
                if (value < 0)
                    throw new System.ArgumentException("Item's count can't be less than 0!");
                _count = value;
                UpdateUI();
            }
        }

        public virtual void Initialize(Item item, int count)
        {
            Item = item;
            Count = count;
            UpdateUI();
        }

        public virtual void UpdateUI()
        {
            if (Item == null) return;

            if (_nameText != null)
                _nameText.text = Item.Name;
            if (_countText != null)
                _countText.text = $"x{Count}";
            if (_icon != null)
                _icon.sprite = Item.Sprite;
        }
    }
}