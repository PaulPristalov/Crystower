using UnityEngine;

[CreateAssetMenu(fileName = "BuildingItem", menuName = "Scriptable Objects/BuildingItem")]
public class BuildingItem : InventoryItem
{
    [SerializeField] private int _health = 10;
    [SerializeField] private int _damage = 5; // Some fields are not for all buildings
    [SerializeField] private float _attackSpeed = 2f;
    [SerializeField] private BuildingType _type = BuildingType.Shooting;
    [SerializeField] private GroundType _ground = GroundType.Grass;
    [SerializeField] private int _energy = 0;
    [SerializeField] private int _resourceCost; // Change the type
    [SerializeField] private int _itemsToUpgrade = 3;
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

public enum GroundType 
{ 
    Grass,
    Water,
    Mountain
}
