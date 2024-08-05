using Inventory.Interaction;
using Inventory.Screen;
using UnityEngine;

namespace Inventory
{
    public class BootstrapInventoryService : MonoBehaviour
    {
        [SerializeField] private InventoryServiceProvider _inventoryServiceProvider;
        [SerializeField] private ItemHandler[] _itemHandlers;
        [SerializeField] private ScreenView _screenView; 

        private void Awake() => Initialize();

        private async void Initialize()
        {
            await _inventoryServiceProvider.Initialize();

            // удалить после проверки
            if (!_inventoryServiceProvider.HasInventories()) 
                _inventoryServiceProvider.PrintInventories();
    
            foreach (var itemHandler in _itemHandlers)
                itemHandler.Initialize(_inventoryServiceProvider);
    
            if (!_inventoryServiceProvider.HasInventories()) 
                _inventoryServiceProvider.PrintInventories();
        }
    }
}