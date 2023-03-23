using System;
using Toolbox.MethodExtensions;
using UnityEngine;


public class HandCollision : MonoBehaviour
{
    [SerializeField] private bool isRightHand;
    [SerializeField] private ConfigurableJoint shoulderJoint;
    
    private GameObject _grabbedObject;
    private Rigidbody _rigidbody;

    private int _isMouseRight;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(_isMouseRight))
        {
            if (_isMouseRight == 0 && !isRightHand) shoulderJoint.targetRotation = Quaternion.Euler(90f, 0f, 0f);
            else if (_isMouseRight == 1 && isRightHand) shoulderJoint.targetRotation = Quaternion.Euler(90f, 0f, 0f);

            if (_grabbedObject != null && !_grabbedObject.HasComponent<FixedJoint>())
            {
                FixedJoint fixedJoint = _grabbedObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = _rigidbody;
            }
        }

        if (Input.GetMouseButtonUp(_isMouseRight))
        {
            if (_isMouseRight == 0) shoulderJoint.targetRotation = Quaternion.Euler(0f, 0f, 0f);
            
            if (_grabbedObject != null) _grabbedObject.RemoveComponent<FixedJoint>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.HasComponent<IGrabbable>()) return;
        _grabbedObject = collision.transform.gameObject;
    }

    private void OnCollisionExit(Collision other)
    {
        _grabbedObject = null;
    }
}