using System;
using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private HandType handType;
        [SerializeField] private ConfigurableJoint shoulderJoint;
        [SerializeField] private GameObject grabbedPivot;

        private Quaternion grabDirection;
        private Entity _playerEntity;

        private GameObject _grabbedObject;
        private Grabbable _grabbedGrabbable;
        private Rigidbody _rigidbody;
        private FixedJoint _grabbedObjectJoined;

        private bool _isGrabbingObject = false;
        private bool _isGrabButtonPressed = false;

        public UnityEvent onGrab;

        private void Awake()
        {
            _playerEntity = GetComponentInParent<Entity>();
            _rigidbody ??= GetComponent<Rigidbody>();
            grabDirection = (handType == HandType.Right) ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, -90, 0);
        }

        private void Update()
        {
            //check if the object is destroyed if so release it
            if (_isGrabbingObject && _grabbedObject == null)
            {
                _isGrabbingObject = false;
                _grabbedGrabbable.OnReleased?.Invoke();
                _grabbedGrabbable = null;
                _grabbedObject = null;
            }

            if (_isGrabButtonPressed) HandlePressed();
        }

        /// <summary>
        /// OnRightGrab is an event method from PlayerInput that sets the grabButton
        /// boolean if the player grabs with their right hand
        /// </summary>
        /// <param name="value"></param>
        private void OnRightGrab(InputValue value)
        {
            if (handType != HandType.Right) return;
            _isGrabButtonPressed = value.isPressed;
            if (!value.isPressed) HandleRelease();
        }

        /// <summary>
        /// OnLeftGrab is an event method from PlayerInput that sets the grabButton
        /// boolean if the player grabs with their left hand
        /// </summary>
        /// <param name="value"></param>
        private void OnLeftGrab(InputValue value)
        {
            if (handType != HandType.Left) return;
            _isGrabButtonPressed = value.isPressed;
            if (!value.isPressed) HandleRelease();
        }

        /// <summary>
        /// OnGrabDirection is an event method from PlayerInput that sets the correct arm rotation for grabbing
        /// </summary>
        /// <param name="value"></param>
        private void OnGrabDirection(InputValue value)
        {
            if (value == null) return;
            Vector2 direction = value.Get<Vector2>();
            grabDirection = handType switch
            {
                HandType.Right => Quaternion.Euler(direction.y * 45, 90 - direction.x * 45, 0f),
                HandType.Left => Quaternion.Euler(direction.y * 45, -90 - direction.x * 45, 0f),
                _ => grabDirection
            };
        }

        /// <summary>
        /// Handles the releasing of objects and resetting the arm position of player
        /// </summary>
        private void HandleRelease()
        {
            shoulderJoint.targetRotation = Quaternion.Euler(0, 0, 0);
            if (_grabbedObject is null) return;

            _grabbedGrabbable.OnReleased?.Invoke();
            Destroy(_grabbedObjectJoined);

            _grabbedGrabbable.RemoveGrabbedBy(_playerEntity.Uuid);
            _grabbedObject = null;
            _isGrabbingObject = false;
            _grabbedGrabbable = null;
        }

        /// <summary>
        /// Handles the grabbing of objects and setting the arm position of player
        /// </summary>
        private void HandlePressed()
        {
            shoulderJoint.targetRotation = grabDirection;

            if (_isGrabbingObject) return;
            if (_grabbedObject == null) return;

            _grabbedGrabbable.AddGrabbedBy(_playerEntity.Uuid);
            if (_grabbedGrabbable.SnapToPivot)
            {
                _grabbedObject.transform.position = grabbedPivot.transform.position;
            }
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), _grabbedObject.GetComponent<Collider>());
            _grabbedObjectJoined = _grabbedObject.AddComponent<FixedJoint>();
            _grabbedObjectJoined.connectedBody = _rigidbody;
            _grabbedGrabbable.OnGrabbed?.Invoke();
            onGrab.Invoke();
            _isGrabbingObject = true;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (!collision.transform.TryGetComponent<Grabbable>(out var grabbable)) return;
            if (_isGrabbingObject) return;

            _grabbedObject = collision.transform.gameObject;
            _grabbedGrabbable = grabbable;
        }

        private void OnTriggerExit(Collider collision)
        {
            if (_isGrabbingObject) return;
            if (collision.transform.gameObject != _grabbedObject) return;

            _grabbedObject = null;
            _grabbedGrabbable = null;
        }

        public Grabbable GrabbedGrabbable => _grabbedGrabbable;
    }
}