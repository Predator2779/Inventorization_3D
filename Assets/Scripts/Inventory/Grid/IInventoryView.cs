using Inventory.Slot;

namespace Inventory.Grid
{
    public interface IInventoryView
    {
        public string OwnerId { get; set; }

        public InventorySlotView GetInventorySlotView(int index);
    }
}