using System;
using UnityEngine;

namespace Inventory.Interaction
{
    public class InventoryInput : MonoBehaviour // to ITickable
    {
        public event Action OnItemInteract; // UniRx?
        public event Action OnInventoryChecked;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) OnItemInteract?.Invoke();
            if (Input.GetKeyDown(KeyCode.Tab)) OnInventoryChecked?.Invoke();
        }
    }
}