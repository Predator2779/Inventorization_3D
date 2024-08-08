using System;
using DG.Tweening;
using Global;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.Slot
{
    public class InventorySlotView : MonoBehaviour, IInventorySlotView, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _amount;
        [SerializeField] private Image _background;
        
        private float _baseFade;
        
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
            _baseFade = _background.color.a;
            SetFade(0, 0);
            SetFade(_baseFade, 0.3f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, 0.1f);
        }

        private void SetFade(float fade, float duration)
        {
            _title.DOFade(fade, duration);
            _amount.DOFade(fade, duration);
            _background.DOFade(fade, duration);
        }
    }
}