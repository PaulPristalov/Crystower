using GameScene.BuildingSystem;
using UnityEngine;

namespace MainMenu.Inventory
{
    [CreateAssetMenu(fileName = "BuildingItem", menuName = "Scriptable Objects/BuildingItem")]
    public class BuildingItem : Item
    {
        [field: SerializeField] public Building Prefab { get; private set; }
    }

    public enum BuildingType
    {
        None,
        Shooting,
        Passive,
        Generator,
        Zone,
        Trap
    }
}
