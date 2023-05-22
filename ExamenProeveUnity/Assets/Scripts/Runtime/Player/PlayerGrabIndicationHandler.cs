using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities.MethodExtensions;

namespace Runtime.Player
{
    public class PlayerGrabIndicationHandler : MonoBehaviour
    {
        [SerializeField] private List<PlayerGrab> _playerGrabs = new();
        
        public UnityEvent onPlayerInRange;
        public UnityEvent onPlayerOutRange;

        private void Update()
        {
            if (_playerGrabs.IsEmpty()) return;
            
            int grabbables = 0;
            foreach (var playerGrab in _playerGrabs)
            {
                if (playerGrab.CanGrabObject || playerGrab.GrabbedGrabbable != null) grabbables++;
            }

            if (grabbables >= 1)
            {
                onPlayerInRange.Invoke();
                return;
            }
            onPlayerOutRange.Invoke();
        }
    }
}