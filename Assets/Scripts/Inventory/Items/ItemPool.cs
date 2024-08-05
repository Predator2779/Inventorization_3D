using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Items
{
    public class ItemPool<T> where T : Item
    {
        private Transform _itemsParent;
        private List<T> _items = new List<T>();

        public ItemPool(Transform itemsParent)
        {
            _itemsParent = itemsParent;
        }

        public void Add(T item)
        {
            if (_items.Contains(item)) return;

            _items.Add(item);
            item.transform.SetParent(_itemsParent);
            item.gameObject.SetActive(false);
        }

        public void Remove(T item, Transform parent = null)
        {
            if (!_items.Contains(item)) return;

            item.gameObject.SetActive(true);
            item.transform.SetParent(parent);
            _items.Remove(item);
        }
    }
}