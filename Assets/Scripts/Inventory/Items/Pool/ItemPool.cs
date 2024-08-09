using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Items.Pool
{
    public class ItemPool<T> where T : Item
    {
        private Transform _itemsParent;
        private Transform _parentOfSpawnedItems;
        private List<T> _items = new List<T>();

        public ItemPool(Transform itemsParent, Transform parentOfSpawnedItems)
        {
            _itemsParent = itemsParent;
            _parentOfSpawnedItems = parentOfSpawnedItems;
        }

        public void Add(T item)
        {
            if (_items.Contains(item)) throw new Exception("Pool not contains this item");

            _items.Add(item);
            item.transform.SetParent(_itemsParent);
            item.SetActivity(false);
        }

        public Item Get(string itemId, Transform parent = null)
        {
            foreach (var item in _items.Where(i => i.Data.Name == itemId))
                return SpawnItem(item);

            throw new Exception("Pool not contains this item");
        }

        public Item Get(Transform parent = null)
        {
            try
            {
                return SpawnItem(_items[0], parent);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }

        public bool HasSpawnedItems() => _parentOfSpawnedItems.childCount > 0;

        private Item SpawnItem(T item, Transform parent = null)
        {
            _items.Remove(item);
            item.transform.SetParent(parent == null ? _parentOfSpawnedItems : parent);
            item.SetActivity(true);
            return item;
        }
    }
}