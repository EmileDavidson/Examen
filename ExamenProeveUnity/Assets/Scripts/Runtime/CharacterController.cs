using UnityEngine;

namespace Runtime
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private Rigidbody hip;
        [SerializeField] private Animator targetAnimator;

        private bool _walk;
        private static readonly int Walk = Animator.StringToHash("Walk");

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
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
