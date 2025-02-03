using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [ContextMenu("Destroy")]
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
