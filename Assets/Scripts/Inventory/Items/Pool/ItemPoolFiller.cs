using UnityEngine;

namespace Inventory.Items.Pool
{
    public class ItemPoolFiller : MonoBehaviour
    {
        [SerializeField] private SpawnAreaOption _spawnAreaOption;
        [SerializeField] private SpawnItemStruct[] _spawnedItems;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _spawnAreaOption.Color;
            Gizmos.DrawCube(_spawnAreaOption.Position, _spawnAreaOption.Scale);
        }
#endif

        public void FillingPool(ref ItemPool<Item> pool)
        {
            foreach (var spawnedItem in _spawnedItems)
            {
                for (int i = 0; i < spawnedItem.CountOfSpawn; i++)
                {
                    pool.Add(Instantiate(
                        spawnedItem.Item,
                        GetRandomPosition(),
                        Quaternion.identity));
                }
            }
        }

        private Vector3 GetRandomPosition()
        {
            var result = Vector3.zero;
            var pos = _spawnAreaOption.Position;
            var scale = _spawnAreaOption.Scale;

            Operation randPos = (position, scale) => Random.Range(position - scale / 2, position + scale / 2);

            result.x = randPos(pos.x, scale.x);
            result.y = randPos(pos.y, scale.y);
            result.z = randPos(pos.z, scale.z);

            return result;
        }

        delegate float Operation(float position, float scale);
    }
}