﻿using System;
using Runtime.Enums;
using Toolbox.MethodExtensions;
using UnityEditor;
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
        [SerializeField] private GameObject grabbedPivot;

        private Quaternion grabDirection;

        private GameObject _grabbedObject;
        private Grabbable _grabbedGrabbable;
        private Rigidbody _rigidbody;
        private FixedJoint _grabbedObjectJoined;

        private bool _isGrabbingObject = false;
        private bool _isGrabButtonPressed = false;

        private void Awake()
        {
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

        private void OnRightGrab(InputValue value)
        {
            if (handType != HandType.Right) return;
            _isGrabButtonPressed = value.isPressed;
            if (!value.isPressed) HandleRelease();
        }

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

        private void HandleRelease()
        {
            shoulderJoint.targetRotation = Quaternion.Euler(0, 0, 0);
            if (_grabbedObject is null) return;

            Destroy(_grabbedObjectJoined);
            _grabbedObject = null;
            _isGrabbingObject = false;
            _grabbedGrabbable.OnReleased?.Invoke();
            _grabbedGrabbable = null;
        }

        private void HandlePressed()
        {
            shoulderJoint.targetRotation = grabDirection;

            if (_isGrabbingObject) return;
            if (_grabbedObject is null) return;

            _grabbedObject.transform.position = grabbedPivot.transform.position;
            _grabbedObjectJoined = _grabbedObject.AddComponent<FixedJoint>();
            _grabbedObjectJoined.connectedBody = _rigidbody;
            _grabbedGrabbable.OnGrabbed?.Invoke();
            _isGrabbingObject = true;
        }

        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (!collision.transform.TryGetComponent<Grabbable>(out var grabbable)) return;
        //     _grabbedObject = collision.transform.gameObject;
        //     _grabbedGrabbable = grabbable;
        // }
        //
        // private void OnCollisionExit(Collision other)
        // {
        //     if (_isGrabbingObject) return;
        //     if (other.transform.gameObject != _grabbedObject) return;
        //
        //     _grabbedObject = null;
        //     _grabbedGrabbable = null;
        // }

        private void OnTriggerEnter(Collider collision)
        {
            if (!collision.transform.TryGetComponent<Grabbable>(out var grabbable)) return;
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
    }
}