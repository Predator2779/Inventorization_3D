using Inventory.Slot;
using TMPro;
using UnityEngine;

namespace Inventory.InventoryGrid
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private TMP_Text _owner;
        [SerializeField] private InventorySlotView[] _slots;

        public string OwnerId
        {
            get => _owner.text;
            set => _owner.text = value;
        }

        public InventorySlotView GetInventorySlotView(int index) => _slots[index];
    }
}