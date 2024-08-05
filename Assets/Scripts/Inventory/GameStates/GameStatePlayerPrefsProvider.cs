using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Inventory.Grid;
using UnityEngine;

namespace Inventory.GameStates
{
    public class GameStatePlayerPrefsProvider : IGameStateProvider, IGameStateSaver
    {
        private const string KEY = "GAME_STATE";

        public GameStateData GameState { get; private set; }

        public async UniTask SaveGameState()
        {
            var json = JsonUtility.ToJson(GameState);
            PlayerPrefs.SetString(KEY, json);
        }

        public async UniTask LoadGameState()
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                var json = PlayerPrefs.GetString(KEY);
                GameState = JsonUtility.FromJson<GameStateData>(json);
            }
            else
            {
                InitializeGameState();
                await SaveGameState();
            }
        }

        public void AddInventory(InventoryGridData inventoryData) => GameState.inventories.Add(inventoryData);

        private void InitializeGameState()
        {
            GameState = new GameStateData
            {
                inventories = new List<InventoryGridData>()
            };
        }
    }
}