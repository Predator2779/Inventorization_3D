using System.Linq;
using Cysharp.Threading.Tasks;
using Inventory.GameStates;
using Inventory.Grid;
using Inventory.Items;
using Inventory.Items.Pool;
using Inventory.ResultStructs;
using Inventory.Screen;
using UnityEngine;

namespace Inventory.Interaction
{
    public class InventoryServiceProvider : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent; // Find or Create

        private ScreenView _screenView;
        private InventoriesService _inventoriesService;
        private GameStatePlayerPrefsProvider _gameStateProvider;
        private GameStateData _gameStateData;
        private ScreenController _screenController;
        private ItemPool<Item> _itemPool;

        public async UniTask Initialize(ScreenView screenView, InventoriesService inventoriesService,
            GameStatePlayerPrefsProvider gameStateProvider, ItemPool<Item> itemPool)
        {
            _screenView = screenView;
            _inventoriesService = inventoriesService;
            _gameStateProvider = gameStateProvider;
            _itemPool = itemPool;

            UpdateGameStateData();
            await InitializeInventories();

            _screenController = new ScreenController(_inventoriesService, _screenView);

            print($"InventoryServiceProvider initialized");
        }

        public async UniTask InitializeInventory(InventoryGridData inventoryData)
        {
            if (_inventoriesService.HasInventory(inventoryData.ownerId))
            {
                Debug.LogWarning($"Inventory {inventoryData.ownerId} is already registered.");
                return;
            }

            _inventoriesService.InitializeInventory(inventoryData);
            _gameStateProvider.AddInventory(inventoryData);

            // обновляем информацию об инвентарях
            UpdateGameStateData();

            print($"Inventory {inventoryData.ownerId} is registered.");
        }

        public void PrintInventories()
        {
            string message = "";
            message += "Registered inventories:\n\n";
            foreach (var data in _gameStateData.inventories)
            {
                message += data.ownerId + "\n";
            }

            print(message);
        }

        public void PrintInventoryContents(string ownerId)
        {
            foreach (var data in _gameStateData.inventories.ToList())
            {
                if (data.ownerId == ownerId && data.slots.Count > 0)
                {
                    string message = "";
                    message += $"{ownerId} inventory contents:\n\n";

                    foreach (var slot in data.slots)
                    {
                        message += $"{slot.itemId} x{slot.amount}\n";
                    }

                    print(message);
                }
            }
        }

        public AddItemToInventoryGridResult AddItemsToInventory(string ownerId, Item item)
        {
            return _inventoriesService.AddItemsToInventory(ownerId, item.Data.Name, item.Data.Amount);
        }

        public RemoveItemFromInventoryGridResult RemoveItemsFromInventory(string ownerId, Item item)
        {
            return _inventoriesService.RemoveItemsFromInventory(ownerId, item.Data.Name, item.Data.Amount);
        }

        public void AddItemToPool(Item item)
        {
            _itemPool.Add(item);
        }

        public Item GetItemFromPool(string itemId)
        {
            return _itemPool.Get(itemId);
        }

        public void OpenInventory(string ownerId)
        {
            _screenController.OpenInventory(ownerId);
        }

        public void CloseInventory()
        {
            _screenController.CloseInventory();
        }

        public bool HasInventories()
        {
            return _gameStateData.inventories.Count > 0;
        }

        private void UpdateGameStateData()
        {
            _gameStateData = _gameStateProvider.GameState;
        }

        private async UniTask InitializeInventories()
        {
            if (_gameStateData.inventories.Count > 0)
            {
                foreach (var inventoryData in _gameStateData.inventories.ToList())
                {
                    await InitializeInventory(inventoryData);
                }
            }
        }
    }
}