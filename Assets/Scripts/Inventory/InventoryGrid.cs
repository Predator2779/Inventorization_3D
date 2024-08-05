using System;
using System.Collections.Generic;
using Inventory.Grid;
using Inventory.ResultStructs;
using Inventory.Slot;
using UnityEngine;

namespace Inventory
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {
        public event Action<string, int> OnItemsAdded;
        public event Action<string, int> OnItemsRemoved;
        public event Action<Vector2Int> OnSizeChanged;

        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new Dictionary<Vector2Int, InventorySlot>();

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;
            CreateSlotsMap();
        }

        public string OwnerId => _data.ownerId;

        public Vector2Int size
        {
            get => _data.size;
            set
            {
                if (value != _data.size)
                {
                    _data.size = value;
                    OnSizeChanged?.Invoke(value);
                }
            }
        }

        public AddItemToInventoryGridResult AddItems(string itemId, int amount = 1)
        {
            var remainingAmount = amount;
            var itemsAddToSlotsWithSameItems = AddToSlotsWithSameItems(itemId, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
                return new AddItemToInventoryGridResult(OwnerId, amount, itemsAddToSlotsWithSameItems);

            var itemsAddedToAvailableSlotsAmount =
                AddToFirstAvailableSlots(itemId, remainingAmount, out remainingAmount);

            var totalAddedItemsAmount = itemsAddToSlotsWithSameItems + itemsAddedToAvailableSlotsAmount;

            return new AddItemToInventoryGridResult(OwnerId, amount, totalAddedItemsAmount);
        }

        public AddItemToInventoryGridResult AddItems(Vector2Int slotPos, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotPos];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty)
                slot.ItemId = itemId;

            var itemSlotCapacity = GetItemSlotCapacity(slot.ItemId);

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemId, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue; // исправить на slot.Amount += newValue;
            }

            return new AddItemToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
        }

        public RemoveItemFromInventoryGridResult RemoveItems(string itemId, int amount = 1)
        {
            if (!Has(itemId, amount))
                return new RemoveItemFromInventoryGridResult(OwnerId, amount, false);

            var amountToRemove = amount;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    var slot = _slotsMap[position];

                    if (slot.ItemId != itemId)
                        continue;

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;
                        RemoveItems(position, itemId, slot.Amount);
                    }
                    else
                    {
                        RemoveItems(position, itemId, amountToRemove);
                        return new RemoveItemFromInventoryGridResult(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception("Something went wrong, couldn't remove some items");
        }

        public RemoveItemFromInventoryGridResult RemoveItems(Vector2Int slotPos, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotPos];

            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
                return new RemoveItemFromInventoryGridResult(OwnerId, amount, false);

            slot.Amount -= amount;

            if (slot.Amount <= 0)
                slot.ItemId = null;

            return new RemoveItemFromInventoryGridResult(OwnerId, amount, true);
        }

        public int GetAmount(string itemId)
        {
            var amount = 0;
            var slots = _data.slots;

            foreach (var slot in slots)
            {
                if (slot.itemId == itemId)
                    amount += slot.amount;
            }

            return amount;
        }

        public bool Has(string itemId, int amount)
        {
            var amountExist = GetAmount(itemId);
            return amountExist >= amount;
        }

        public void SwitchSlots(Vector2Int firstSlotPos, Vector2Int secondSlotPos)
        {
            var firstSlot = _slotsMap[firstSlotPos];
            var secondSlot = _slotsMap[secondSlotPos];
            var tempSlotItemId = firstSlot.ItemId;
            var tempSlotAmount = firstSlot.Amount;
            firstSlot.ItemId = secondSlot.ItemId;
            firstSlot.Amount = secondSlot.Amount;
            secondSlot.ItemId = tempSlotItemId;
            secondSlot.Amount = tempSlotAmount;
        }

        public void SetSize(Vector2Int newSize)
        {
            // передавать в аргументы конфиг с размерами (small, middle, large)
            throw new NotImplementedException();
        }

        public IReadOnlyInventorySlot[,] GetSlots()
        {
            // создаем копию инвентаря, чтобы нельзя было изменить по ссылке
            var array = new IReadOnlyInventorySlot[size.x, size.y];
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    array[x, y] = _slotsMap[position];
                }

            return array;
        }

        public bool TryAddItems(string itemId, int amount)
        {
            throw new NotImplementedException();
        }

        private void CreateSlotsMap()
        {
            var size = _data.size;
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                {
                    var index = x * size.y + y; // перевод двумерного массива в одномерный

                    if (_data.slots.Count < index + 1) continue;

                    var slotData = _data.slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(x, y);

                    _slotsMap[position] = slot;
                }
        }

        private int AddToSlotsWithSameItems(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    var slot = _slotsMap[position];

                    if (slot.IsEmpty)
                        continue;

                    var slotItemCapacity = GetItemSlotCapacity(itemId);
                    if (slot.Amount >= slotItemCapacity)
                        continue;

                    if (slot.ItemId != itemId)
                        continue;

                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                            return itemsAddedAmount;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }


        private int AddToFirstAvailableSlots(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var position = new Vector2Int(x, y);
                    var slot = _slotsMap[position];

                    if (!slot.IsEmpty)
                        continue;

                    slot.ItemId = itemId;
                    var newValue = remainingAmount;
                    var slotItemCapacity = GetItemSlotCapacity(itemId);

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int GetItemSlotCapacity(string itemId)
        {
            return 99;
        }
    }
}