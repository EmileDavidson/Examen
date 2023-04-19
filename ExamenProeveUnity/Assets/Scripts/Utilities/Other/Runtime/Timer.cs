using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Other.Runtime
{
    [Serializable]
    public class Timer
    {
        [field: SerializeField] private float wantedTime;
    
        private float _currentTime;
        private bool _isFinished = false;
        private bool _canceled = false;
    
        public UnityEvent onTimerFinished = new();
        public UnityEvent<float> onTimerUpdate = new();

        public Timer(float timeInSeconds)
        {
            wantedTime = timeInSeconds;
            _currentTime = 0;
        }

        public void Update(float deltaTime)
        {
            if (_isFinished || Canceled) return;
        
            _currentTime += deltaTime;

            onTimerUpdate.Invoke(_currentTime / wantedTime);
            if (_currentTime >= wantedTime)
            {
                FinishTimer();
            }
        }

        private void FinishTimer()
        {
            _isFinished = true;
            onTimerFinished.Invoke();
        }

        public float WantedTime => wantedTime;

        public bool IsFinished => _isFinished;

        public bool Canceled
        {
            get => _canceled;
            set => _canceled = value;
        }
    }
}