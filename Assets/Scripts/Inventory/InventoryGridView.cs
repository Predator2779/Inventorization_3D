using Inventory.Grid;
using UnityEngine;

namespace Inventory
{
    public class InventoryGridView : MonoBehaviour
    {
        private IReadOnlyInventoryGrid _inventory;

        public void Setup(IReadOnlyInventoryGrid inventory)
        {
            _inventory = inventory;
            PrintSlots();
        }

        public void PrintSlots()
        {
            var slots = _inventory.GetSlots();
            var size = _inventory.size;
            var result = "";

            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                {
                    var slot = slots[x, y];
                    result += ($"Slot: ({x}:{y}). Item: {slot.ItemId}. Amount: {slot.Amount}\n");
                }

            print(result);
        }
    }
}