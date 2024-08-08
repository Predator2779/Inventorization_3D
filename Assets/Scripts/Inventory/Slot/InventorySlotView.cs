using System;
using DG.Tweening;
using Global;
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
            SetFade(0, 0);
            SetFade(1, 0.3f);
        }

        private void OnDisable()
        {
            SetFade(0, 0);
        }

        private void SetFade(float fade, float duration)
        {
            _title.DOFade(fade, duration);
            _amount.DOFade(fade, duration);
            _background.DOFade(fade, duration);
        }
    }
}