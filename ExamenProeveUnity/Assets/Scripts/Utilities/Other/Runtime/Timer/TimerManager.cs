using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Other.Runtime.Timer
{
    /// <summary>
    /// Timer manager is used to manage multiple timers at once and update them all at once with the same deltaTime
    /// it also has a super timer that triggers each update of the other timers and has the longest wanted time of all timers in the list of timers
    /// so you can use it to trigger something after all timers are finished
    /// </summary>
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private List<Timer> timers = new();
        [SerializeField] private Timer superTimer;
        
        private void Awake()
        {
            var longestTimer = timers.OrderByDescending(timer => timer.WantedTime).First();
            
            superTimer.SetWantedTime(longestTimer.WantedTime, true);
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