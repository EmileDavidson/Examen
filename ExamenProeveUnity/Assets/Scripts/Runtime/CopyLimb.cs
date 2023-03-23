using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///Copy limbs does what it says.
///it will copy the rotation of the limb you want to copy and apply it to the limb you want to copy it to.
///this can be used to copy the values from an animated character to a physics character.
public class CopyLimb : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;

    private ConfigurableJoint _configurableJoint;
    private Quaternion _targetInitialRotation;
    
    void Start() //todo check if this can be awake.
    {
        this._configurableJoint = this.GetComponent<ConfigurableJoint>();
        this._targetInitialRotation = this.targetLimb.transform.localRotation;
    }

    private void FixedUpdate() {
        this._configurableJoint.targetRotation = CopyRotation();
    }

    private Quaternion CopyRotation() {
        return Quaternion.Inverse(this.targetLimb.localRotation) * this._targetInitialRotation;
    }
}
