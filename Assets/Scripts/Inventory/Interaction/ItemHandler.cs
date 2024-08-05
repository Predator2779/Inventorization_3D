using System;
using System.Collections.Generic;
using Global;
using Inventory.Grid;
using Inventory.Items;
using Inventory.Slot;
using UnityEngine;

namespace Inventory.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class ItemHandler : MonoBehaviour
    {
        [SerializeField] private string _ownerId;

        private InventoryServiceProvider _inventoryServiceProvider;
        private InventoryInput _inventoryInput;
        private List<Item> _selectedItems = new List<Item>();
        private bool _isInventoryOpened;

        // [Inject]
        // private void Construct(InventoryServiceProvider provider, InventoryInput input)
        // {
        //     _inventoryServiceProvider = provider;
        //     _inventoryInput = input;
        //     Initialize();
        // }

        private void OnTriggerEnter(Collider other) => AddSelectedItem(other.GetComponent<Item>());
        private void OnTriggerExit(Collider other) => RemoveSelectedItem(other.GetComponent<Item>());

        public void Initialize(InventoryServiceProvider inventoryServiceProvider)
        {
            // _inventoryInput = new InventoryInput();
            _inventoryInput = gameObject.AddComponent<InventoryInput>();
            _inventoryInput.OnItemInteract += AddItemToInventory;
            _inventoryInput.OnInventoryChecked += CheckInventory;

            // зарегистрировать инвентарь
            var inventoryData = new InventoryGridData
            {
                ownerId = _ownerId,
                size = GlobalConstants.InventorySize,
                slots = GetSlotData(GlobalConstants.InventorySize)/////////////////////////////////////
            };

            _inventoryServiceProvider = inventoryServiceProvider;
            _inventoryServiceProvider.InitializeInventory(inventoryData);
            _inventoryServiceProvider.PrintInventoryContents(_ownerId);

            print($"{_ownerId} ItemHandler initialized");
        }

        private List<InventorySlotData> GetSlotData(Vector2Int size) /////////////// временно
        {
            var slotData = new List<InventorySlotData>();
            var length = size.x * size.y;
            
            for (int i = 0; i < length; i++)
            {
                slotData.Add(new InventorySlotData()
                {
                    itemId = "",
                    amount = 0
                });
            }

            return slotData;
        }
        
        private void AddItemToInventory()
        {
            if (_selectedItems.Count > 0)
            {
                var item = _selectedItems[0];
                var result = _inventoryServiceProvider.AddItemsToInventory(_ownerId, item);
                if (result.ItemsToAddAmount == result.ItemsAddedAmount)
                {
                    RemoveSelectedItem(item);
                    //  и после вернуть обратно в пул
                }
                else
                {
                    // добавить в _selectedItems оставшиеся
                    throw new Exception("Not all items have been added");
                }

                _inventoryServiceProvider.PrintInventoryContents(_ownerId);
            }
        }

        private void CheckInventory()
        {
            _isInventoryOpened = !_isInventoryOpened;

            if (_isInventoryOpened) _inventoryServiceProvider.OpenInventory(_ownerId);
            else _inventoryServiceProvider.CloseInventory();
        }

        private void AddSelectedItem(Item item)
        {
            if (item != null && !_selectedItems.Contains(item)) _selectedItems.Add(item);
        }

        private void RemoveSelectedItem(Item item)
        {
            if (item != null && _selectedItems.Contains(item)) _selectedItems.Remove(item);
        }
    }
}