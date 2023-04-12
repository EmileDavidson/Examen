using System;
using Runtime.Enums;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Runtime.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private HandType handType;
        [SerializeField] private ConfigurableJoint shoulderJoint;

        private GameObject _grabbedObject;
        private Grabbable _grabbedGrabbable;
        private Rigidbody _rigidbody;
        private FixedJoint _grabbedObjectJoined;

        private bool _isGrabbingObject = false;
        private bool _isGrabButtonPressed = false;

        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(_isGrabButtonPressed) HandlePressed();

            //check if the object is destroyed if so release it
            if (_isGrabbingObject && _grabbedObject == null)
            {
                _isGrabbingObject = false;
                _grabbedGrabbable.OnReleased?.Invoke();
                _grabbedGrabbable = null;
            }
        }

        private void OnRightGrab(InputValue value)
        {
            if (handType != HandType.Right) return;
            _isGrabButtonPressed = value.isPressed;
            if(!value.isPressed) HandleRelease();
        }
        
        private void OnLeftGrab(InputValue value)
        {
            if (handType != HandType.Left) return;
            _isGrabButtonPressed = value.isPressed;
            if(!value.isPressed) HandleRelease();
        }

        private void HandleRelease()
        {
            shoulderJoint.targetRotation = Quaternion.Euler(0f, 0f, 0f);
            if (_grabbedObject is null) return;

            Destroy(_grabbedObjectJoined);
            _grabbedObject = null;
            _isGrabbingObject = false;
            _grabbedGrabbable.OnReleased?.Invoke();
            _grabbedGrabbable = null;
        }

        private void HandlePressed()
        {
            shoulderJoint.targetRotation = Quaternion.Euler(90f, 0f, 0f);

            if(_isGrabbingObject) return;
            if (_grabbedObject is null) return;

            _grabbedObjectJoined = _grabbedObject.AddComponent<FixedJoint>();
            _grabbedObjectJoined.connectedBody = _rigidbody;
            _grabbedGrabbable.OnGrabbed?.Invoke();
            _isGrabbingObject = true;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.TryGetComponent<Grabbable>(out var grabbable)) return;
            _grabbedObject = collision.transform.gameObject;
            _grabbedGrabbable = grabbable;
        }

        private void OnCollisionExit(Collision other)
        {
            if (_isGrabbingObject) return;
            if (other.transform.gameObject != _grabbedObject) return;
            
            _grabbedObject = null;
            _grabbedGrabbable = null;
        }
    }
}