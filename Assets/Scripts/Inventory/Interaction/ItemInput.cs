using System;
using UniRx;
using UnityEngine;

namespace Inventory.Interaction
{
    public class InventoryInput
    {
        public event Action OnItemInteract;
        public event Action OnInventoryChecked;

        public InventoryInput()
        {
            Observable
                .EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.E))
                .Subscribe(_ => OnItemInteract?.Invoke());

            Observable
                .EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Tab))
                .Subscribe(_ => OnInventoryChecked?.Invoke()); 
            
            Observable
                .EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(_ => Application.Quit());
        }
    }
}