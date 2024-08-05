using System.Collections.Generic;
using Inventory.Slot;

namespace Inventory.InventoryGrid
{
    public class InventoryGridController
    {
        private readonly List<InventorySlotController> _slotControllers = new List<InventorySlotController>();
        
        public InventoryGridController(IReadOnlyInventoryGrid inventory, IInventoryView view)
        {
            view.OwnerId = inventory.OwnerId;
            CreateGrid(inventory, view);
        }

        private void CreateGrid(IReadOnlyInventoryGrid inventory, IInventoryView view)
        {
            var size = inventory.size;
            var slots = inventory.GetSlots();
            var lineLength = size.y;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var index = x * lineLength + y;
                    var slotView = view.GetInventorySlotView(index);
                    var slot = slots[x, y];
                    _slotControllers.Add(new InventorySlotController(slot, slotView));
                }
            }  
        }
    }
}