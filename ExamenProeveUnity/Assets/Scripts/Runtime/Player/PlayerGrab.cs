using Runtime.Enums;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private MouseType mouseType;
        [SerializeField] private ConfigurableJoint shoulderJoint;

        private GameObject _grabbedObject;
        private Rigidbody _rigidbody;
        private FixedJoint _grabbedObjectJoined;

        private bool _isGrabbingObject = false;

        private void Awake()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        private void OnRightGrab(InputValue value)
        {
            if (mouseType != MouseType.Right) return;
            if(value.isPressed) HandleMousePressed();
            else HandleMouseRelease();
        }
        
        private void OnLeftGrab(InputValue value)
        {
            if (mouseType != MouseType.Left) return;
            if(value.isPressed) HandleMousePressed();
            else HandleMouseRelease();
        }

        private void HandleMouseRelease()
        {
            shoulderJoint.targetRotation = Quaternion.Euler(0f, 0f, 0f);
            if (_grabbedObject is null) return;

            Destroy(_grabbedObjectJoined);
            _grabbedObject = null;
            _isGrabbingObject = false;
        }

        private void HandleMousePressed()
        {
            shoulderJoint.targetRotation = Quaternion.Euler(90f, 0f, 0f);

            if(_isGrabbingObject) return;
            if (_grabbedObject is null) return;

            _grabbedObjectJoined = _grabbedObject.AddComponent<FixedJoint>();
            _grabbedObjectJoined.connectedBody = _rigidbody;
            _isGrabbingObject = true;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.HasComponent<Grabbable>()) return;
            _grabbedObject = collision.transform.gameObject;
        }

        private void OnCollisionExit(Collision other)
        {
            if (_isGrabbingObject) return;
            if (other.transform.gameObject != _grabbedObject) return;
            
            _grabbedObject = null;
        }
    }
}