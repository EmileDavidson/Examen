using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using Utilities.MethodExtensions;
using Utilities.Other.Runtime;

namespace Runtime.Managers
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        private List<Entity> _players = new();
        private List<PlayerInput> _playerInputs = new();

        protected override void Awake()
        {
            base.Awake();
            _playerInputs = FindObjectsOfType<PlayerInput>().ToList();
            foreach (var playerInput in _playerInputs)
            {
                _players.AddIfNotNull(playerInput.GetComponent<Entity>());
            }
            HandlePlayerSpawning();
        }
        
        private void HandlePlayerSpawning()
        {
            var spawnLocations = LevelManager.Instance.SpawnLocations;
            if (spawnLocations.IsEmpty()) return;
            if (_playerInputs == null || _playerInputs.IsEmpty()) return;

            foreach (var playerInput in _playerInputs)
            {
                playerInput.transform.position = spawnLocations.Get(playerInput.playerIndex).position;
            }
        }

        public List<Entity> Players => _players;
        public List<PlayerInput> PlayerInputs => _playerInputs;

        public List<PlayerInput> GetInputs(bool forceFind = false)
        {
            if (forceFind)
            {
                _playerInputs = FindObjectsOfType<PlayerInput>().ToList();
                foreach (var playerInput in _playerInputs)
                {
                    _players.AddIfNotNull(playerInput.GetComponent<Entity>());
                }

                return _playerInputs;
            }
            
            return _playerInputs;
        }
    }
}