using System;
using System.Collections.Generic;
using DG.Tweening;
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

        private void OnTriggerEnter(Collider other) => AddSelectedItem(other.GetComponent<Item>());
        private void OnTriggerExit(Collider other) => RemoveSelectedItem(other.GetComponent<Item>());

        public void Initialize(InventoryServiceProvider inventoryServiceProvider)
        {
            _inventoryInput = new InventoryInput();
            _inventoryInput.OnItemInteract += AddItemToInventory;
            _inventoryInput.OnInventoryChecked += CheckInventory;

            var inventoryData = new InventoryGridData
            {
                ownerId = _ownerId,
                size = GlobalConstants.InventorySize,
                slots = InitializeSlots(GlobalConstants.InventorySize)
            };

            _inventoryServiceProvider = inventoryServiceProvider;
            _inventoryServiceProvider.InitializeInventory(inventoryData);
            _inventoryServiceProvider.PrintInventoryContents(_ownerId);

            print($"{_ownerId} inventory initialized");
        }

        private List<InventorySlotData> InitializeSlots(Vector2Int size)
        {
            var slotData = new List<InventorySlotData>();
            var length = size.x * size.y;

            for (int i = 0; i < length; i++)
                slotData.Add(new InventorySlotData());

            return slotData;
        }

        private void AddItemToInventory()
        {
            if (_selectedItems.Count > 0)
            {
                var item = _selectedItems[0];
                _selectedItems.Remove(item);
                var result = _inventoryServiceProvider.AddItemsToInventory(_ownerId, item);
                if (result.ItemsToAddAmount == result.ItemsAddedAmount)
                {
                    RemoveSelectedItem(item);
                    
                    var seq = DOTween.Sequence();
                    seq.Append(item.transform.DOScale(Vector3.zero, 0.5f));
                    seq.AppendCallback(() => { _inventoryServiceProvider.AddItemToPool(item); });
                }
                else
                {
                    item.Data.Amount = result.ItemsNotAddedAmount;
                    _selectedItems.Add(item);
                    print("Not all items have been added");
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