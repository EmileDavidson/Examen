using Toolbox.Utils.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Managers
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        [SerializeField] private int score = 0;
        [SerializeField] private int minScore = -100;
        [SerializeField] private int maxScore = 100;
    
        private int _money = 0;
    
        public UnityEvent onMoneyChange = new();
        public UnityEvent onScoreChange = new();

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
