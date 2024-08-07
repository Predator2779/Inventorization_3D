using System;
using Inventory.Grid;

namespace Inventory.Screen
{
    public class ScreenController
    {
        private readonly InventoriesService _inventoriesService;
        private readonly ScreenView _view;

        private InventoryGridController _currentInventoryController;
        
        public ScreenController(InventoriesService inventoriesService, ScreenView view)
        {
            _inventoriesService = inventoriesService;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            CloseInventory();

            _view.SetActivity(true);
            var inventory = _inventoriesService.GetInventory(ownerId);
            var inventoryView = _view.GetInventoryView();

            _currentInventoryController = new InventoryGridController(inventory, inventoryView);
        }

        public void CloseInventory()
        {
            _view.SetActivity(false);
            _currentInventoryController = null;
        }
    }
}