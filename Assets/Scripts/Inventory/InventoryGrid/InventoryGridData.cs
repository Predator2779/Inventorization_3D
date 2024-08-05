using System;
using System.Collections.Generic;
using Inventory.Slot;
using UnityEngine;

namespace Inventory.InventoryGrid
{
    [Serializable]
    public class InventoryGridData
    {
        public string ownerId;
        public List<InventorySlotData> slots;
        public Vector2Int size;
    }
}