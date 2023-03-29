using UnityEngine;

namespace Runtime
{
    public class CopyLimb : MonoBehaviour
    {
        [SerializeField] private Transform targetLimb;
    
        private ConfigurableJoint _configurableJoint;
        private Quaternion _targetInitialRotation;
        // Start is called before the first frame update
        void Start()
        {
            _configurableJoint = GetComponent<ConfigurableJoint>();
            _targetInitialRotation = targetLimb.transform.localRotation;
        }

        private void FixedUpdate() {
            _configurableJoint.targetRotation = CopyRotation();
        }

        private Quaternion CopyRotation() {
            return Quaternion.Inverse(targetLimb.localRotation) * _targetInitialRotation;
        }
    }
}
