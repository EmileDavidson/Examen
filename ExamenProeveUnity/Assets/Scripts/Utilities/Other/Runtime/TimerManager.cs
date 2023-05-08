using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Utilities.Other.Runtime
{
    /// <summary>
    /// Timer manager is basically a list of timers so we can do things like 'waves' of customers spawning or
    /// something else that requires (multiple) timers. 
    /// </summary>
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private List<Timer> timers = new List<Timer>();
        [SerializeField] private Timer superTimer;
        private Timer _longestTimer;

        private void Awake()
        {
            _longestTimer = timers.OrderByDescending(timer => timer.WantedTime).First();
            
            superTimer.SetWantedTime(_longestTimer.WantedTime, true);
            superTimer.onTimerUpdate.AddListener((_) =>
            {
                timers.ForEach(timer => timer.Update(Time.deltaTime));
            });
        }

        private void Update()
        {
            if (superTimer.IsFinished) return;
            superTimer.Update(Time.deltaTime);
        }
    }
}