using System;

namespace Inventory.InventoryGrid
{
    public interface IReadOnlyInventory
    {
        public event Action<string, int> OnItemsAdded;
        public event Action<string, int> OnItemsRemoved;

        public string OwnerId { get; }
        public int GetAmount(string itemId);
        public bool Has(string itemId, int amount);
    }
}