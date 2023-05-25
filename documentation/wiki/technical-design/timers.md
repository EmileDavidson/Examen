# Timers

Our game has a lot of timers that make the customer go to the next state or spawn over time. to handle all these things that want a timer we created the timer class!&#x20;

The timer class takes a wanted time and should be updated each update using Time.DeltaTime as a parameter. When the timer updates, the timer updated event triggers having a float as value. The float is a 0 to 1 value where 0 is not started and 1 finished.

The timer also has an onTimerFinished event that gets triggered when the timer finishes. After the timer is finished, triggering update does not work since it is finished.

This timer is great for easy and simple timers.&#x20;

```csharp
﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Other.Runtime.Timer
{
    [Serializable]
    public class Timer
    {
        [field: SerializeField] private float wantedTime;

        private float _currentTime;
        private bool _canceled;

        public UnityEvent onTimerFinished = new();
        public UnityEvent<float> onTimerUpdate = new();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timeInSeconds"></param>
        public Timer(float timeInSeconds)
        {
            wantedTime = timeInSeconds;
            _currentTime = 0;
        }

        /// <summary>
        /// Used from outside to update the timer with the given deltaTime (Time.deltaTime)
        /// if the timer is finished it will invoke the onTimerFinished event and won't update anymore
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            if (IsFinished || Canceled) return;

            _currentTime += deltaTime;

            onTimerUpdate.Invoke(_currentTime / wantedTime);
            if (IsFinished)
            {
                onTimerFinished.Invoke();
            }
        }

        /// <summary>
        /// Sets the wanted time for the timer to finish to given time
        /// resets the current timer to 0 if resetCurrentTimer is true
        /// </summary>
        /// <param name="time"></param>
        /// <param name="resetCurrentTimer"></param>
        public void SetWantedTime(float time, bool resetCurrentTimer = false)
        {
            wantedTime = time;
            if (resetCurrentTimer)
            {
                _currentTime = 0;
            }
        }

        /// <summary>
        /// Cancels the timer so it won't update anymore and won't finish
        /// </summary>
        public void Cancel()
        {
            _canceled = true;
        }

        /// <summary>
        /// Resets the timer to the default values so it will update again and can finish
        /// </summary>
        public void ResetTimer()
        {
            _currentTime = 0;
            _canceled = false;
        }

        #region getters & setters
        public float WantedTime => wantedTime;
        public bool IsFinished => _currentTime >= wantedTime;
        public bool Canceled => _canceled;
        #endregion // getters & setters
    }
}
```
