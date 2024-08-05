﻿using Inventory.Slot;

namespace Inventory.InventoryGrid
{
    public interface IInventoryView
    {
        public string OwnerId { get; set; }

        public InventorySlotView GetInventorySlotView(int index);
    }
}