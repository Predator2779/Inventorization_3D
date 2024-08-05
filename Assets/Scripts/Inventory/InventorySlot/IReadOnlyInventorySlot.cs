using System;

namespace Inventory.Slot
{
    public interface IReadOnlyInventorySlot
    {
        public event Action<string> OnItemIdChanged;
        public event Action<int> OnItemAmountChanged;
        
        public string ItemId { get; }
        public int Amount { get; }
        public bool IsEmpty { get; }
    }
}