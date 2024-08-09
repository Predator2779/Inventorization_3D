using System;
using DG.Tweening;
using UnityEngine;

namespace Inventory.Items
{
    [RequireComponent(typeof(Collider))]
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemData Data { get; private set; }

        private void OnEnable()
        {
            transform.DOScale(Vector3.one, 0.5f);
        }
    }
}