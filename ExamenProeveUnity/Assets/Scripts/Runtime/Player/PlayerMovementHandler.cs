using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private float speed = 10f;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private Rigidbody hipRigidbody;
        [SerializeField] private Animator targetAnimator;

        [SerializeField] private float minPowerValue = .2f;
     
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
            if (Math.Abs(value.x) < minPowerValue) value.x = 0;
            if (Math.Abs(value.y) < minPowerValue) value.y = 0;
            
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
            
            ConstrainPlayer(!_walk);

            if (_walk)
            {
                var targetAngle = (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg) - 90;
                hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
                hipRigidbody.velocity += direction * speed;
                hipRigidbody.velocity = Vector3.ClampMagnitude(hipRigidbody.velocity, speed);
            }

            targetAnimator.SetBool(Walk, _walk);
        }
        
        /// <summary>
        /// Sets the correct restraining to the player when stopping or starting to move
        /// </summary>
        /// <param name="setConstrained"></param>
        private void ConstrainPlayer(bool setConstrained)
        {
            RigidbodyConstraints constraints = hipRigidbody.constraints;
            if (setConstrained)
            {
                constraints = RigidbodyConstraints.FreezePosition;
            }
            else
            {
                bool isMovingHorizontally = _horizontalMoveValue != 0f;
                bool isMovingVertically = _verticalMoveValue != 0f;


                if (isMovingHorizontally && isMovingVertically)
                {
                    constraints = RigidbodyConstraints.FreezePositionY;
                }
                else if (isMovingHorizontally)
                {
                    constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                }
                else if (isMovingVertically)
                {
                    constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                }
            }

            hipRigidbody.constraints = constraints;
        }
    }
}