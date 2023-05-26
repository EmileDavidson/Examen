using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputEvents : MonoBehaviour
    {
        public UnityEvent interact = new UnityEvent();

        public void OnInteractionButton()
        {
            interact.Invoke();
        }
    }
}