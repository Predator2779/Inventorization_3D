using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Slot
{
    public class InventorySlotView : MonoBehaviour, IInventorySlotView
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _amount;
        [SerializeField] private Image _background;

        public string Title
        {
            get => _title.text;
            set => _title.text = value;
        }

        public int Amount
        {
            get => Convert.ToInt32(_amount.text);
            set => _amount.text = value == 0 ? "" : value.ToString();
        }

        private void OnEnable()
        {
            _title.DOFade(1, 1);
            _amount.DOFade(1, 1);
            _background.DOFade(1, 1);
        }

        private void OnDisable()
        {
            _title.DOFade(0, 0);
            _amount.DOFade(0, 0);
            _background.DOFade(0, 0);
        }
    }
}