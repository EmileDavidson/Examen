using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
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

        public UnityEvent onDestinationReached = new UnityEvent();
        private Path _path;

        private static readonly int Walk = Animator.StringToHash("Walk");
        private bool _walk = false;
        
        private List<int> blockingPoints = new();
        
        public Path Path
        {
            get => _path;
            set
            {
                if (Equals(_path, value)) return;
                blockingPoints.ForEach(element => grid.GetNodeByIndex(element).IsTempBlocked = false);
                blockingPoints.Clear();
                _path = value;
            }
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            grid ??= WorldManager.Instance.worldGrid;
        }
        
        /// <summary>
        /// FixedUpdate is called once per physics frame
        /// </summary>
        private void FixedUpdate()
        {
            if (Path?.PathNodes == null || Path.PathNodes.IsEmpty() || Path.CurrentIndex == -1 || grid.GetNodeByIndex(Path.GetNextNode()).IsTempBlocked)
            {
                _walk = false;
                targetAnimator.SetBool(Walk, _walk);
                return;
            }

            Vector3 playerPos = hipRb.gameObject.transform.position;
            int nextPathNodeIndex = Path.GetNextNode();
            var nextPos = grid.GetWorldPositionOfNode(grid.GetNodeByIndex(nextPathNodeIndex).GridPosition);
            nextPos.y = playerPos.y;
            
            //handle walk 
            var gotoPosition = Vector3.MoveTowards(playerPos, nextPos, 0.1f);
            var direction = (gotoPosition - playerPos).normalized;
            _walk = (direction.magnitude >= 0.1f);
            
            //handle rotation
            var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            hipRb.gameObject.transform.position = Vector3.MoveTowards(playerPos, nextPos, 0.085f);
            
            targetAnimator.SetBool(Walk, _walk);
            
            ValidateNextPointReached(playerPos, nextPathNodeIndex);
        }

        /// <summary>
        /// Checks if the player position is close enough to the next path node to be considered as reached
        /// and updates the path index if so.
        /// it also triggers the onDestinationReached event if the last node is reached.
        /// </summary>
        /// <param name="playerPos"></param>
        /// <param name="nextPathNodeIndex"></param>
        private void ValidateNextPointReached(Vector3 playerPos, int nextPathNodeIndex)
        {
            playerPos.y = 0; 
            if (!(Vector3.Distance(playerPos, grid.GetWorldPositionOfNode(grid.GetNodeByIndex(nextPathNodeIndex).GridPosition)) < .4f))
            {
                return;
            }

            grid.GetNodeByIndex(nextPathNodeIndex).SetTempBlock(true);
            grid.GetNodeByIndex(Path.PathNodes[Path.CurrentIndex]).SetTempBlock(false);
            
            blockingPoints.Add(nextPathNodeIndex);
            blockingPoints.Remove(Path.PathNodes[Path.CurrentIndex]);

            Path.CurrentIndex++;
            if (Path.CurrentIndex < Path.PathNodes.Count - 1) return;

            Path.CurrentIndex = -1;
            Path.DestinationReached = true;
            onDestinationReached.Invoke();
        }
    }
}