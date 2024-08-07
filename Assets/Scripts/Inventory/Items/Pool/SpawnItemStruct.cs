using System;

namespace Inventory.Items.Pool
{
    [Serializable]
    public struct SpawnItemStruct
    {
        public Item Item;
        public int CountOfSpawn;
    }
}