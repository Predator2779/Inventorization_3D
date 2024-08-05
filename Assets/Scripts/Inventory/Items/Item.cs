using UnityEngine;

namespace Inventory.Items
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemData Data { get; private set; }
    }
}