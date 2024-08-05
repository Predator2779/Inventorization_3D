namespace Inventory.Slot
{
    public interface IInventorySlotView
    {
        public string Title { get; set; }
        public int Amount { get; set; }
    }
}