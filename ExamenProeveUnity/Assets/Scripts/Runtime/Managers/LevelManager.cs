using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utilities.MethodExtensions;
using Utilities.Other.Runtime;

namespace Runtime.Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        /// <summary>
        /// All spawn locations for the players. gets used in order of the list.
        /// </summary>
        [SerializeField] private List<Transform> spawnLocations = new List<Transform>();

        [SerializeField] private List<int> scorePercentagePerRating = new List<int>()
        {
            50,
            75,
            90
        };

        /// <summary>
        /// Type, score, minScore, maxScore 
        /// </summary>
        private Dictionary<ScoreType, Tuple<int, int, int>> _score = new Dictionary<ScoreType, Tuple<int, int, int>>();

        /// <summary>
        /// The money the player has, has earned and spent in this level
        /// </summary>
        private int _money = 0;
        private int _moneyEarned = 0;
        private int _moneySpent = 0;

        public UnityEvent onMoneyChange = new();
        public UnityEvent onScoreChange = new();

        public int Money
        {
            get => _money;
            set
            {
                if (_money < value) _moneyEarned += value - _money;
                else _moneySpent += _money - value;
                _money = value;
                onMoneyChange?.Invoke();
            }
        }

        public int MoneyEarned => _moneyEarned;
        public int MoneySpent => _moneySpent;

        public void AddScore(int addScore, int minScore, int maxScore, ScoreType type = ScoreType.General)
        {
            if (_score.ContainsKey(type))
            {
                _score[type] = new Tuple<int, int, int>(_score[type].Item1 + addScore, _score[type].Item2 + minScore, _score[type].Item3 + maxScore);
                onScoreChange.Invoke();
                return;
            }   
            
            _score[type] = new Tuple<int, int, int>(addScore, minScore, maxScore);
            onScoreChange.Invoke();
        }

        public Tuple<int, int, int> GetAllScore(ScoreType type)
        {
            return _score[type];
        }

        public int GetScore(ScoreType type)
        {
            return _score[type].Item1;
        }

        public int GetMinScore(ScoreType type)
        {
            return _score[type].Item2;
        }

        
        public int GetMaxScore(ScoreType type)
        {
            return _score[type].Item3;
        }

        public int GetScorePercentage(ScoreType type)
        {
            int calculateScore = GetScore(type) + Math.Abs(GetMinScore(type));
            int maxScore = GetMaxScore(type) + Math.Abs(GetMinScore(type));
            
            return Mathf.RoundToInt((float) calculateScore / maxScore * 100);   
        }

        public int GetTotalScorePercentage()
        {
            int count = _score.Count;
            int addedPercentage = 0;
            foreach (var keyValue in _score)
            {
                var key = keyValue.Key;
                var value = keyValue.Value;
                addedPercentage += GetScorePercentage(key);
            }

            return addedPercentage / count;
        }
        
        public int GetStarRating()
        {
            int percentage = GetTotalScorePercentage();
            return scorePercentagePerRating.Count(ratePercentage => percentage > ratePercentage);
        }
        
        public List<ScoreType> GetAllUsedScoreTypes()
        {
            return _score.Keys.ToList();
        }

        public List<Transform> SpawnLocations => spawnLocations;
    }
}