using System;
using UnityEngine;

namespace Inventory.Items.Pool
{
    [Serializable]
    public struct SpawnItemStruct
    {
        public Item Item;
        [Range(0, 1000)] public int CountToPool;
        [Range(0, 1000)] public int CountOfSpawn;
    }
}