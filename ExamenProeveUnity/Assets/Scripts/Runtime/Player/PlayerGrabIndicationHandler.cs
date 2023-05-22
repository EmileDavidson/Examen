using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using Utilities.MethodExtensions;

namespace Runtime.Player
{
    public class PlayerGrabIndicationHandler : MonoBehaviour
    {
        [SerializeField] private List<PlayerGrab> playerGrabs = new();

        private int _grabbables;
        private int _previousGrabbables = 0;
        
        public UnityEvent onPlayerInRange;
        public UnityEvent onPlayerOutRange;

        private void Awake()
        {
            if (playerGrabs.IsEmpty()) this.enabled = false;
        }

        private void Update()
        {
            _grabbables = 0;
            foreach (var playerGrab in playerGrabs)
            {
                if (playerGrab.ObjectIsInRange || playerGrab.GrabbedGrabbable != null) _grabbables++;
            }
            if (_grabbables != _previousGrabbables) GrabbableChanged();
            _previousGrabbables = _grabbables;
        }

        private void GrabbableChanged()
        {
            if (_grabbables >= 1)
            {
                onPlayerInRange.Invoke();
                return;
            }
            onPlayerOutRange.Invoke();
        }
    }
}