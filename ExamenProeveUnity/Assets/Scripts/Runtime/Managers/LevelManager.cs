using System.Collections.Generic;
using Toolbox.MethodExtensions;
using Toolbox.Utils.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        /// <summary>
        /// All spawn locations for the players. gets used in order of the list.
        /// </summary>
        [SerializeField] private List<Transform> spawnLocations = new List<Transform>();

        /// <summary>
        /// Score of the player in the current level. 
        /// </summary>
        private int _score = 0;

        /// <summary>
        /// The min amount of score the player can have.
        /// </summary>
        private int _minScore = 0;

        /// <summary>
        /// The max amount of score the player can have.
        /// </summary>
        private int _maxScore = 0;

        /// <summary>
        /// The money the player has in this level
        /// </summary>
        private int _money = 0;

        public UnityEvent onMoneyChange = new();
        public UnityEvent onScoreChange = new();

        protected override void Awake()
        {
            base.Awake();
            HandlePlayerSpawning();
        }

        private void HandlePlayerSpawning()
        {
            var players = FindObjectsOfType<PlayerInput>();
            if (spawnLocations.IsEmpty()) return;
            if (players is null || players.IsEmpty()) return;

            foreach (var playerInput in players)
            {
                playerInput.transform.position = spawnLocations.Get(playerInput.playerIndex).position;
            }
        }


        public int Money
        {
            get => _money;
            set
            {
                _money = value;
                onMoneyChange?.Invoke();
            }
        }

        public int Score
        {
            get => _score;
            private set => _score = Mathf.Clamp(value, _minScore, _maxScore);
        }

        public void AddScore(int given, int min, int max)
        {
            _score += given;
            _minScore += min;
            _maxScore += max;
            onScoreChange?.Invoke();
        }
    }
}