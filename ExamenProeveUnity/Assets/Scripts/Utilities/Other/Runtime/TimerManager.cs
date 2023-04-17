using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Other.Runtime
{
    /// <summary>
    /// Timer manager is basically a list of timers so we can do things like 'waves' of customers spawning or
    /// something else that requires (multiple) timers. 
    /// </summary>
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private List<Timer> timers = new List<Timer>();
        
        private Timer _longestTimer;
        private Timer _shortestTimer;
        private Timer _superTimer;

        private void Awake()
        {
            _longestTimer = timers.OrderByDescending(timer => timer.WantedTime).First();
            _shortestTimer = timers.OrderBy(timer => timer.WantedTime).First();
            
            _superTimer = new Timer(_longestTimer.WantedTime);
            
            timers.ForEach(timer => timer.onTimerFinished.AddListener(() =>
            {
                Debug.Log(" timer finished ");
            }));
  
            _superTimer.onTimerUpdate.AddListener((_) =>
            {
                timers.ForEach(timer => timer.Update(Time.deltaTime));
            });

            //A bit of context here: I want to force finish all the timers since the super timer is the same time as the longest timer 
            //but the 'child timer' doesn't always trigger the onTimerFinished event. 
            _superTimer.onTimerFinishing.AddListener(() =>
            {
                timers.ForEach(timer => timer.ForceFinish());
            });
        }

        private void Update()
        {
            if (_superTimer.IsFinished) return;
            _superTimer.Update(Time.deltaTime);
        }

        public Timer SuperTimer => _superTimer;
    }
}