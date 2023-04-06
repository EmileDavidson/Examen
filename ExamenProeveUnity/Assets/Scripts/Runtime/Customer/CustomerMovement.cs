using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Customer
{
    public class CustomerMovement : MonoBehaviour
    {
        [SerializeField] private MyGrid grid;
        [SerializeField] private Rigidbody hipRb;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private Animator targetAnimator;

        private bool _isWaitingForTempBlock = false;

        private static readonly int Walk = Animator.StringToHash("Walk");
        private bool _walk = false;

        public UnityEvent onDestinationReached = new UnityEvent();

        public Path Path { get; set; }

        private void Awake()
        {
            grid ??= WorldManager.Instance.worldGrid;
        }

        private void FixedUpdate()
        {
            if (Path == null || Path.PathNodes == null || Path.PathNodes.IsEmpty()) return;
            if (Path.CurrentIndex == -1)
            {
                //stop the player from walking when the path is not set
                _walk = false;
                targetAnimator.SetBool(Walk, _walk);
                return;
            }

            if (_isWaitingForTempBlock && grid.GetNodeByIndex(Path.PathNodes[Path.CurrentIndex + 1]).IsTempBlocked)
            {
                return;
            }

            Vector3 playerPos = hipRb.gameObject.transform.position;
            int nextPathNodeIndex = Path.GetNextNode();
            var nextPos = grid.GetWorldPositionOfNode(grid.GetNodeByIndex(nextPathNodeIndex).GridPosition);
            nextPos.y = playerPos.y;

            var gotoPosition = Vector3.MoveTowards(playerPos, nextPos, 0.1f);
            var direction = (gotoPosition - playerPos).normalized;

            _walk = (direction.magnitude >= 0.1f);
            var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            hipRb.gameObject.transform.position = Vector3.MoveTowards(playerPos, nextPos, 0.085f);

            targetAnimator.SetBool(Walk, _walk);

            playerPos.y = 0; // ground the player position since we are on a 2D grid
            if (!(Vector3.Distance(playerPos,
                    grid.GetWorldPositionOfNode(grid.GetNodeByIndex(nextPathNodeIndex).GridPosition)) < .4f))
            {
                return;
            }

            if (grid.GetNodeByIndex(nextPathNodeIndex).IsTempBlocked)
            {
                _isWaitingForTempBlock = true;
                _walk = false;
                targetAnimator.SetBool(Walk, _walk);
                return;
            }

            _isWaitingForTempBlock = false;

            grid.GetNodeByIndex(nextPathNodeIndex).SetTempBlock(true);
            grid.GetNodeByIndex(Path.PathNodes[Path.CurrentIndex]).SetTempBlock(false);

            Path.CurrentIndex++;
            if (Path.CurrentIndex < Path.PathNodes.Count - 1) return;
            Path.CurrentIndex = -1;
            Path.DestinationReached = true;
            onDestinationReached.Invoke();
        }
    }
}