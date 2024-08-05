using System;
using Inventory.Slot;
using UnityEngine;

namespace Inventory.Grid
{
    public interface IReadOnlyInventoryGrid : IReadOnlyInventory
    {
        public event Action<Vector2Int> OnSizeChanged;
        
        public Vector2Int size { get; }
        public IReadOnlyInventorySlot[,] GetSlots();
    }
}