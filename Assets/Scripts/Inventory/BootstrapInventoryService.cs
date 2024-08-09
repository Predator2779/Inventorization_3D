using Cysharp.Threading.Tasks;
using Inventory.GameStates;
using Inventory.Interaction;
using Inventory.Items;
using Inventory.Items.Pool;
using Inventory.Screen;
using UnityEngine;

namespace Inventory
{
    public class BootstrapInventoryService : MonoBehaviour
    {
        [SerializeField] private InventoryServiceProvider _inventoryServiceProvider;
        [SerializeField] private ItemHandler[] _itemHandlers;
        [SerializeField] private ScreenView _screenView;
        [SerializeField] private ItemPoolFiller _itemPoolFiller;
        [SerializeField] private Transform _poolOfItems;
        [SerializeField] private Transform _poolOfSpawnedItems;
        
        private void Awake() => Initialize();

        private async void Initialize()
        {
            var gameStateProvider = await InitializeGameStateProvider();
            var inventoriesService = InitializeInventoryService(gameStateProvider);
            var itemPool = InitializeItemsPool(_poolOfItems, _poolOfSpawnedItems);
            
            _itemPoolFiller.FillingPool(ref itemPool);
            new RespawnerItems(itemPool, 5);
            
            await InitializeInventoryServiceProvider(inventoriesService, gameStateProvider, itemPool);
            InitializeItemHandlers();

            if (!_inventoryServiceProvider.HasInventories())
                _inventoryServiceProvider.PrintInventories();
        }

        private async UniTask<GameStatePlayerPrefsProvider> InitializeGameStateProvider()
        {
            var gameStateProvider = new GameStatePlayerPrefsProvider();
            await gameStateProvider.LoadGameState();
            return gameStateProvider;
        }

        private InventoriesService InitializeInventoryService(IGameStateSaver stateSaver)
        {
            return new InventoriesService(stateSaver);
        }

        private async UniTask InitializeInventoryServiceProvider(InventoriesService inventoriesService,
            GameStatePlayerPrefsProvider gameStateProvider, ItemPool<Item> itemPool)
        {
            await _inventoryServiceProvider.Initialize(_screenView, inventoriesService, gameStateProvider, itemPool);
        }

        private void InitializeItemHandlers()
        {
            foreach (var itemHandler in _itemHandlers)
                itemHandler.Initialize(_inventoryServiceProvider);
        }

        private ItemPool<Item> InitializeItemsPool(Transform poolOfItems, Transform poolOfSpawnedItems)
        {
            return new ItemPool<Item>(poolOfItems, poolOfSpawnedItems);
        }
    }
}