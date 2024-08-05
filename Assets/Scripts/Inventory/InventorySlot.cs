using System;
using Inventory.Slot;

namespace Inventory
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        public event Action<string> OnItemIdChanged;
        public event Action<int> OnItemAmountChanged;

        private readonly InventorySlotData _data;

        public InventorySlot(InventorySlotData data)
        {
            _data = data;
        }

        public string ItemId
        {
            get => _data.itemId;
            set
            {
                if (value != _data.itemId)
                {
                    _data.itemId = value;
                    OnItemIdChanged?.Invoke(value);
                }
            }
        }

        public int Amount
        {
            get => _data.amount;
            set
            {
                if (value != _data.amount)
                {
                    _data.amount = value;
                    OnItemAmountChanged?.Invoke(value);
                }
            }
        }

        public bool IsEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);
    }
}