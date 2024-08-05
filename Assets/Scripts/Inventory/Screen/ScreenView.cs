using Inventory.Grid;
using UnityEngine;

namespace Inventory.Screen
{
    public class ScreenView : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;

        public InventoryView GetInventoryView() => _inventoryView;
    }
}