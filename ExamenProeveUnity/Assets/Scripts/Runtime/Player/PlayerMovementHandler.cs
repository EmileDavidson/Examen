﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private float speed = 5f;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private Rigidbody hip;
        [SerializeField] private Animator targetAnimator;

        private float _horizontalMoveValue = 0f;
        private float _verticalMoveValue = 0f;

        private bool _walk;
        private static readonly int Walk = Animator.StringToHash("Walk");

        /// <summary>
        /// OnMovement is an event method from PlayerInput that is called when the player uses the move input action
        /// </summary>
        /// <param name="context"></param>
        public void OnMovement(InputValue context)
        {
            var value = context.Get<Vector2>();
            _horizontalMoveValue = value.x;
            _verticalMoveValue = value.y;
        }

        /// <summary>
        /// Update is called once per frame
        /// And it will update the movement of the player
        /// </summary>
        private void Update()
        {
            Vector3 direction = new Vector3(_horizontalMoveValue, 0f, _verticalMoveValue).normalized;
            _walk = (direction.magnitude >= 0.1f);

            if (_walk)
            {
                var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
                hip.AddForce(direction * speed);
            }

            targetAnimator.SetBool(Walk, _walk);
        }
    }
}