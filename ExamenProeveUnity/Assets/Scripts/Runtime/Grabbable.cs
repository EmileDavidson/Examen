using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime
{
    /// <summary>
    /// Grabbable is a script you can put on objects that can be grabbed by the player.
    /// note that the gameobject needs a collider that is a trigger for this to work. 
    /// </summary>
    public class Grabbable : MonoBehaviour, IGrabbable
    {
        [SerializeField] private bool snapToPivot = false;
        private bool _isGrabbed = false;
    
        public readonly UnityEvent OnGrabbed = new UnityEvent();
        public readonly UnityEvent OnReleased = new UnityEvent();

        private void Awake()
        {
            _isGrabbed = false;
        
            OnGrabbed?.AddListener(() =>
            {
                _isGrabbed = true;
            });
            OnReleased?.AddListener(() =>
            {
                _isGrabbed = false;
            });
        }

        public bool SnapToPivot => snapToPivot;
        public bool IsGrabbed => _isGrabbed;
    }
}
