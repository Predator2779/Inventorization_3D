using Cysharp.Threading.Tasks;

namespace Inventory.GameStates
{
    public interface IGameStateSaver
    {
        public UniTask SaveGameState();
    }
}