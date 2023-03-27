using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private Rigidbody hip;
        [SerializeField] private Animator targetAnimator;
        [SerializeField] private PlayerInputHandler playerInputHandler;

        private float _horizontalMoveValue = 0f;
        private float _verticalMoveValue = 0f;

        private bool _walk;
        private static readonly int Walk = Animator.StringToHash("Walk");

        private void Start()
        {
            SetupInputHandler();
        }

        private void SetupInputHandler()
        {
            playerInputHandler ??= GetComponent<PlayerInputHandler>();
            if (playerInputHandler is null)
            {
                Debug.LogWarning("playerInputHandler is null");
                return;
            }

            playerInputHandler.onMoveValueChanged.AddListener((value) =>
            {
                _horizontalMoveValue = value.x;
                _verticalMoveValue = value.y;
            });

            playerInputHandler.onMoveCanceled.AddListener(() =>
            {
                _horizontalMoveValue = 0f;
                _verticalMoveValue = 0f;
            });
        }

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