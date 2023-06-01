using System.Collections.Generic;
using TMPro;
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
        [SerializeField] private bool dontDestroyPrefabsOnLoad = true;

        [SerializeField] private TMP_Text connectionCountText;
        [SerializeField] private List<TMP_Text> connectionTexts = new List<TMP_Text>();


        private int _spawnIndex = 1;
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

            if (dontDestroyPrefabsOnLoad)
            {
                DontDestroyOnLoad(playerInput.gameObject);
            }

            handleInterfaceOnJoin(playerInput);
        }

        private void OnPlayerLeft(PlayerInput playerInput)
        {
            if (playerInput.playerIndex == _spawnIndex) return;
            _spawnIndex--;
            _playerInputManager.playerPrefab = playerPrefabs.Get(_spawnIndex);

            handleInterfaceOnLeft(playerInput);
        }

        private void ValidatePrefab()
        {
            List<GameObject> removeList = new List<GameObject>();
            foreach (var playerPrefab in playerPrefabs)
            {
                if (playerPrefab == null) continue;
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


        private void handleInterfaceOnLeft(PlayerInput playerInput)
        {
            int index = playerInput.playerIndex;
            if (connectionCountText != null)
            {
                connectionCountText.text = $"Connected: {_playerInputManager.playerCount}";
            }

            if (!connectionTexts.ContainsSlot(index))
            {
                Debug.LogWarning("Could not change connection text because the index is out of range");
                return;
            }

            connectionTexts[index].text = "Disconnected";
        }

        private void handleInterfaceOnJoin(PlayerInput playerInput)
        {
            int index = playerInput.playerIndex;
            var device = playerInput.devices[0]?.device;

            if (connectionCountText != null)
            {
                connectionCountText.text = $"Connected: {_playerInputManager.playerCount}";
            }

            if (!connectionTexts.ContainsSlot(index))
            {
                Debug.LogWarning("Not enough connection texts to display all players");
                return;
            }

            connectionTexts[index].text = device?.displayName ?? "Unknown";
        }
    }
}