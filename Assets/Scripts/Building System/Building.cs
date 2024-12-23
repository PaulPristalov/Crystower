using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    /// <summary>
    /// Size of the building in cells.
    /// </summary>
    [SerializeField] private Vector2Int _size = new Vector2Int(2, 2);
    [SerializeField] private int HP = 5;

    public Vector2Int Size => _size;
    public bool Placed { get; private set; } = false;

    public UnityEvent<Building> OnPlaced;
    public UnityEvent<Building> OnDestroying;

    // Test
    [SerializeField] private float rotation_speed = 5f;

    private void Update()
    {
        // Test
        transform.Rotate(0f, 0f, rotation_speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        Placed = false;
        OnDestroying?.Invoke(this);
    }

    [ContextMenu("Destroy")]
    private void Destroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Makes the building placed and initialize it.
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    public void Place()
    {
        if (Placed)
            throw new System.Exception("Building is already placed!");

        Placed = true;
        OnPlaced?.Invoke(this);
    }
}
