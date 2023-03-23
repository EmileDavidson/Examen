using UnityEngine;

public class CharacterPickup : MonoBehaviour
{
    private Rigidbody _rb;
    private int _leftOrRight;

    [SerializeField] private ConfigurableJoint rightArmJoint;
    [SerializeField] private ConfigurableJoint leftArmJoint;

    private GameObject _grabbedObject;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftOrRight))
        {
            if (_leftOrRight == 0)
            {
                leftArmJoint.targetRotation = Quaternion.Euler(90f, 0f, 0f);
                if (_grabbedObject != null)
                {
                    FixedJoint fixedJoint = _grabbedObject.AddComponent<FixedJoint>();
                    fixedJoint.connectedBody = _rb;
                    fixedJoint.breakForce = 9001;
                }
            }

            if (_leftOrRight == 1) rightArmJoint.targetRotation = Quaternion.Euler(0f, 90f, 0f);
        }
        
        if (Input.GetMouseButtonUp(_leftOrRight))
        {
            if (_leftOrRight == 0)
            {
                leftArmJoint.targetRotation = Quaternion.Euler(0f, 0f, 0f);
                Destroy(_grabbedObject.GetComponent<FixedJoint>());
            }

            if (_leftOrRight == 1) rightArmJoint.targetRotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
}
