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

            _grabbedObject.RemoveComponent<FixedJoint>();
            _grabbedObject = null;
        }

        private void HandleMousePressed()
        {
            if (!Input.GetMouseButton(MouseKeyDict.Dict[mouseType])) return;

            shoulderJoint.targetRotation = Quaternion.Euler(90f, 0f, 0f);

            if (_grabbedObject is null) return;

            FixedJoint fixedJoint = _grabbedObject.GetOrAddComponent<FixedJoint>();
            fixedJoint.connectedBody = _rigidbody;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.transform.HasComponent<Grabbable>()) return;
            _grabbedObject = collision.transform.gameObject;
        }
    }
}