using DG.Tweening;
using Inventory.Grid;
using UnityEngine;

namespace Inventory.Screen
{
    public class ScreenView : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;

        public InventoryView GetInventoryView() => _inventoryView;
        private void OnEnable()
        {
            transform.DOScale(Vector3.zero, 0);

            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(new Vector3( 0, 1.5f, 0), 0.05f));
            seq.Append(transform.DOScale(new Vector3(1.5f, 1, 0), 0.1f));
            seq.Append(transform.DOScale(new Vector3(1, 1, 0), 0.1f));
        }
    }
}