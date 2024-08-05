namespace Inventory.Slot
{
    public class InventorySlotController
    {
        private readonly IInventorySlotView _view;

        public InventorySlotController(IReadOnlyInventorySlot slot, IInventorySlotView view)
        {
            _view = view;
            slot.OnItemIdChanged += OnSlotItemIdChanged;
            slot.OnItemAmountChanged += OnSlotItemAmountChanged;

            view.Title = slot.ItemId;
            view.Amount = slot.Amount;
        }

        private void OnSlotItemIdChanged(string newItemId) => _view.Title = newItemId;
        private void OnSlotItemAmountChanged(int newAmount) => _view.Amount = newAmount;
    }
}