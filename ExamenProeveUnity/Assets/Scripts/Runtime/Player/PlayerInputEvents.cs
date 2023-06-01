using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputEvents : MonoBehaviour
    {
        public UnityEvent onRightShoulder = new UnityEvent();
        public UnityEvent onLeftShoulder = new UnityEvent();
        public UnityEvent interact = new UnityEvent();

        public void OnInteractionButton(InputValue value)
        {
            interact.Invoke();
        }
        
        public void OnRightGrab(InputValue value)
        {
            onRightShoulder.Invoke();
        }
        
        public void OnLeftGrab(InputValue value)
        {
            onLeftShoulder.Invoke();
        }
        
        public void OnRightShoulder(InputValue value)
        {
            onRightShoulder.Invoke();
        }
        
        public void OnLeftShoulder(InputValue value)
        {
            onLeftShoulder.Invoke();
        }
    }
}