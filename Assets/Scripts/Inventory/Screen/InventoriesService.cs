using System.Collections.Generic;
using Inventory.Grid;
using Inventory.ResultStructs;
using UnityEngine;

namespace Inventory.Screen
{
    public class InventoriesService
    {
        private readonly IGameStateSaver _gameStateSaver;
        private readonly Dictionary<string, InventoryGrid> _inventoriesMap = new Dictionary<string, InventoryGrid>();

        public InventoriesService(IGameStateSaver gameStateSaver)
        {
            _gameStateSaver = gameStateSaver;
        }

        public InventoryGrid InitializeInventory(InventoryGridData inventoryGridData)
        {
            // добавляем все инвентари в словарь при старте игры
            var inventory = new InventoryGrid(inventoryGridData);
            _inventoriesMap[inventory.OwnerId] = inventory;

            return inventory;
        }

        public AddItemToInventoryGridResult AddItemsToInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.AddItems(itemId, amount);

            _gameStateSaver.SaveGameState();

            return result;
        }

        public AddItemToInventoryGridResult AddItemsToInventory(string ownerId, Vector2Int slotPos, string itemId,
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.AddItems(slotPos, itemId, amount);

            _gameStateSaver.SaveGameState();

            return result;
        }

        public RemoveItemFromInventoryGridResult RemoveItemsFromInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.RemoveItems(itemId, amount);

            _gameStateSaver.SaveGameState();

            return result;
        }

        public RemoveItemFromInventoryGridResult RemoveItemsFromInventory(string ownerId, Vector2Int slotPos,
            string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            var result = inventory.RemoveItems(slotPos, itemId, amount);

            _gameStateSaver.SaveGameState();

            return result;
        }

        public bool HasItem(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }

        public bool HasInventory(string ownerId)
        {
            return _inventoriesMap.ContainsKey(ownerId);
        }

        public IReadOnlyInventoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }
    }
}