using System;
using UnityEngine;


namespace MainMenu.Inventory
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;

        public string Name => _name;
        public Sprite Sprite => _sprite;
    }

    public class SelectedItem
    {
        public Type Type { get; private set; }
        public int Index { get; set; }

        public SelectedItem(int index, Type type)
        {
            Index = index;
            Type = type;
        }
    }
}