using System;

namespace Inventory.Items
{
    [Serializable]
    public struct SpawnItemStruct
    {
        public Item Item;
        public int CountOfSpawn;
    }
}