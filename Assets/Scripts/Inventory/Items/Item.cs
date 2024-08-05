using UnityEngine;

namespace Inventory.Items
{
    [RequireComponent(typeof(Collider))]
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemData Data { get; private set; }
    }
}