using UnityEngine;
using UnityEngine.Events;

namespace Runtime
{
    /// <summary>
    /// Interactable is a script you can put on objects that can be interacted with by the player.
    /// note that the gameObject needs a collider for this to work. and only the closest object to the player will be interacted with.
    ///
    /// this class will also not have any logic but only events that can be subscribed to.
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        public UnityEvent onRightShoulderClicked = new();
        public UnityEvent onLeftShoulderClicked = new();
        public UnityEvent onInteractionClicked = new();
        public UnityEvent onLeftGrab = new();
        public UnityEvent onRightGrab = new();
    }
}