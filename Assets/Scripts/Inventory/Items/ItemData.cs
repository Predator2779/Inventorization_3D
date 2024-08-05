﻿using UnityEngine;

namespace Inventory.Items
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Configs/Data/Items", order = 0)]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; } = "Unnamed";
        [field: SerializeField] public int Count { get; set; } = 1;
    }}