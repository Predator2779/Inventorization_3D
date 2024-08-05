using Cysharp.Threading.Tasks;

namespace Inventory.GameStates
{
    public interface IGameStateProvider
    {
        public UniTask SaveGameState();
        public UniTask LoadGameState();
    }
}