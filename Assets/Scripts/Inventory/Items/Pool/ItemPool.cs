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
            item.gameObject.SetActive(false);
        }

        public Item Get(string itemId, Transform parent = null)
        {
            foreach (var i in _items.Where(i => i.Data.Name == itemId))
            {
                i.transform.SetParent(parent == null ? _parentOfSpawnedItems : parent);
                i.gameObject.SetActive(true);
                _items.Remove(i);
                return i;
            }

            throw new Exception("Pool not contains this item");
        }
    }
}