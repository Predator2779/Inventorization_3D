namespace Inventory.ResultStructs
{
    public struct RemoveItemFromInventoryGridResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsToRemoveAmount;
        public readonly bool Succes;

        public RemoveItemFromInventoryGridResult(string inventoryOwnerId, int itemsToRemoveAmount, bool succes)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToRemoveAmount = itemsToRemoveAmount;
            Succes = succes;
        }
    }
}