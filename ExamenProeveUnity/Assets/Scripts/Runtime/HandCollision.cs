using Runtime.Dictonaries;
using Runtime.Enums;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;

namespace Runtime
{
    [RequireComponent(typeof(Rigidbody))]
    public class HandCollision : MonoBehaviour
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

        private void Update()
        {
            HandleMousePressed();
            HandleMouseRelease();
        }

        private void HandleMouseRelease()
        {
            if (!Input.GetMouseButtonUp(MouseKeyDict.Dict[mouseType])) return;
            
            shoulderJoint.targetRotation = Quaternion.Euler(0f, 0f, 0f);
            
            if (_grabbedObject is null) return;

            Destroy(_grabbedObjectJoined);
            _grabbedObject = null;
            _isGrabbingObject = false;
        }

        private void HandleMousePressed()
        {
            if (!Input.GetMouseButton(MouseKeyDict.Dict[mouseType])) return;
            
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
    }
}