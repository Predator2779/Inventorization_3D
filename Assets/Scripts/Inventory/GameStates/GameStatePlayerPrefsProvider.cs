using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Cysharp.Threading.Tasks;
using Inventory.Grid;
using UnityEngine;

namespace Inventory.GameStates
{
    public class GameStatePlayerPrefsProvider : IGameStateProvider, IGameStateSaver
    {
        private const string KEY = "GAME_STATE";
        private const string _saveFileName = "TestSave.json";

        public GameStateData GameState { get; private set; }

        public async UniTask SaveGameState()
        {
            await UniTask.WaitUntil(() =>
            {
                try
                {
                    var json = JsonUtility.ToJson(GameState);
                    File.WriteAllText(GetSavePath(_saveFileName), json);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                Debug.Log("Saved successfully!");

                // var json = JsonUtility.ToJson(GameState);
                // PlayerPrefs.SetString(KEY, json);
                return true;
            });
        }

        public async UniTask LoadGameState()
        {
            if (!File.Exists(GetSavePath(_saveFileName)))
            {
                Debug.Log("File does not exist!");
                InitializeGameState();
                await SaveGameState();
                return;
            }

            await UniTask.WaitUntil(() =>
            {
                try
                {
                    var json = File.ReadAllText(GetSavePath(_saveFileName));
                    GameState = JsonUtility.FromJson<GameStateData>(json);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                Debug.Log("Game has been loaded.");

                return true;
            });

            // if (PlayerPrefs.HasKey(KEY))
            // {
            //     await UniTask.WaitUntil(() =>
            //     {
            //         var json = PlayerPrefs.GetString(KEY);
            //         GameState = JsonUtility.FromJson<GameStateData>(json);
            //         return true;
            //     });
            // }
            // else
            // {
            //     InitializeGameState();
            //     await SaveGameState();
            // }
        }

        public void AddInventory(InventoryGridData inventoryData)
        {
            if (GameState.inventories.All(inventory => inventory.ownerId != inventoryData.ownerId))
                GameState.inventories.Add(inventoryData);
            else
                Debug.LogWarning("This inventory is already registered in GameStateData!");
        }

        private void InitializeGameState()
        {
            GameState = new GameStateData
            {
                inventories = new List<InventoryGridData>()
            };
        }

        private string GetSavePath(string fileName)
        {
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, fileName);
#else
            return Path.Combine(Application.persistentDataPath, fileName);
#endif
        }
    }
}