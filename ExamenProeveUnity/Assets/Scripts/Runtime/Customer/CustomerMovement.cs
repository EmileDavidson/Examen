using System.Collections.Generic;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Runtime.Customer
{
    [RequireComponent(typeof(PathFinding))]
    public class CustomerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody hipRb;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private PathFinding pathFinding;
        [SerializeField] private Animator targetAnimator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        
        private List<Shelf> _shelves = new();
        private List<CashRegister> _cashRegisters;
        private bool _walk = false;
        
        public UnityEvent onDestinationReached = new UnityEvent();

        private void Awake()
        {
            pathFinding ??= GetComponent<PathFinding>();
            _cashRegisters = new List<CashRegister>(FindObjectsOfType<CashRegister>());
            _shelves = new List<Shelf>(FindObjectsOfType<Shelf>());
        }

        [Button]
        private void Start()
        {
            pathFinding.Path.CurrentIndex = 0;
            
            Vector3 position = transform.position;
            GridNode currentGridNode = pathFinding.GetGrid().GetNodeFromWorldPosition(new Vector3(position.x, 0, position.z));


            Shelf randomShelf = _shelves[Random.Range(0, _shelves.Count)];
            Vector3Int targetGridPosition = randomShelf.InteractPosition;
            GridNode targetGridNode = pathFinding.GetGrid().GetNodeFromWorldPosition(new Vector3(targetGridPosition.x, 0, targetGridPosition.z));

            pathFinding.StartPathfinding(currentGridNode, targetGridNode);
        }

        private void FixedUpdate()
        {
            if (pathFinding == null) return;
            if (pathFinding.GetGrid() == null) return;
            if (pathFinding.Path == null || pathFinding.Path.PathNodes.IsEmpty()) return;
            if (pathFinding.Path.DestinationReached) return;
            Path path = pathFinding.Path;
            
            if (path.CurrentIndex == -1)
            {
                Debug.LogWarning("Could not found next path node");
                return;
            }
            
            Vector3 playerPos = hipRb.gameObject.transform.position;
            var nextPathNode = path.GetNextNode();
            var nextPos = nextPathNode.GetWorldPosition(pathFinding.GetGrid().PivotPoint);

            var gotoPosition = Vector3.MoveTowards(playerPos, nextPos, 0.01f);
            var direction = (gotoPosition - playerPos).normalized;
            
            _walk = (direction.magnitude >= 0.1f);

            if (_walk)
            {
                var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
                hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
                hipRb.gameObject.transform.position = Vector3.MoveTowards(playerPos, nextPos, 0.01f);
            }
            
            targetAnimator.SetBool(Walk, _walk);

            if (!(Vector3.Distance(playerPos, nextPathNode.GetWorldPosition(pathFinding.GetGrid().PivotPoint)) < 0.1f)) return;
            path.CurrentIndex++;
            if (path.CurrentIndex < path.PathNodes.Count - 1) return;
            path.CurrentIndex = -1;
            path.DestinationReached = true;
            onDestinationReached.Invoke();
        }
    }
}