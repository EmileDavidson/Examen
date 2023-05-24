using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.MethodExtensions;

namespace Runtime.Player
{
    /// <summary>
    /// Can be used as an extension for the PlayerInputManager
    /// </summary>
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerInputManagerExtension : MonoBehaviour
    {
        [Tooltip("List of all player prefabs that can be spawned in the game (first prefab can be skipped)")]
        [SerializeField]
        private List<GameObject> playerPrefabs = new();
        [SerializeField] private List<Transform> spawnLocations = new();

        private int _spawnIndex = 0;
        private PlayerInputManager _playerInputManager;

        private void Awake()
        {
            _playerInputManager = GetComponent<PlayerInputManager>();

            ValidatePrefab();
            
            _playerInputManager.playerJoinedEvent.AddListener(OnPlayerJoined);
            _playerInputManager.playerLeftEvent.AddListener(OnPlayerLeft);
        }

        private void OnPlayerJoined(PlayerInput playerInput)
        {
            _spawnIndex++;
            _playerInputManager.playerPrefab = playerPrefabs.Get(_spawnIndex);

            if (spawnLocations.IsNotEmpty())
            {
                playerInput.gameObject.transform.position = spawnLocations.Get(_spawnIndex).position;
            }
        }
        
        private void OnPlayerLeft(PlayerInput playerInput)
        {
            if(playerInput.playerIndex == _spawnIndex) return;
            _spawnIndex--;
            _playerInputManager.playerPrefab = playerPrefabs.Get(_spawnIndex);
        }
        

            private void ValidatePrefab()
        {
            List<GameObject> removeList = new List<GameObject>();
            foreach (var playerPrefab in playerPrefabs)
            {
                if(playerPrefab == null) continue;
                if (playerPrefab.GetComponent<PlayerInput>() != null) continue;
                Debug.LogWarning($"Player prefab {playerPrefab.name} does not have a PlayerInput component!");
                removeList.Add(playerPrefab);
            }

            playerPrefabs.Insert(0, _playerInputManager.playerPrefab);
            playerPrefabs.RemoveAll(x => removeList.Contains(x) || x == null);

            if (playerPrefabs.Count == 0)
            {
                Debug.LogError("No player prefabs found! game can not start!");
            }
        }
    }
}