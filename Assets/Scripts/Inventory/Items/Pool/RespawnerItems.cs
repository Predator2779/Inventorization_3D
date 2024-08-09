using UniRx;

namespace Inventory.Items.Pool
{
    public class RespawnerItems
    {
        private readonly ItemPool<Item> _pool;
        private readonly int _countSpawnedItems;

        public RespawnerItems(ItemPool<Item> pool, int countSpawnedItems)
        {
            _pool = pool;
            _countSpawnedItems = countSpawnedItems;
            
            Observable
                .EveryUpdate()
                .Where(_ => !_pool.HasSpawnedItems())
                .Subscribe(_ => RespawnItems());
        }

        private void RespawnItems()
        {
            for (int i = 0; i < _countSpawnedItems; i++) _pool.Get();
        }
    }
}