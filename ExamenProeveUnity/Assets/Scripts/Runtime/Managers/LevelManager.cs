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
        [SerializeField] private List<Transform> spawnLocations = new List<Transform>();

        [SerializeField] private int score = 0;
        [SerializeField] private int minScore = -100;
        [SerializeField] private int maxScore = 100;
    
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
            get => score;
            set
            {
                if(value > maxScore) value = maxScore;
                if(value < minScore) value = minScore;
            
                score = value;
                onScoreChange?.Invoke();
            }
        }
    }
}
