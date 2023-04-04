using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using TMPro;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Customer
{
    [RequireComponent(typeof(PathFinding))]
    public class CustomerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody hipRb;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private PathFinding pathFinding;

        private List<Shelf> _shelves = new();
        private List<CashRegister> _cashRegisters;
        

        private int currentIndex = -1;
        private bool _destinationReached = false;

        private void Awake()
        {
            pathFinding ??= GetComponent<PathFinding>();
            currentIndex = 0;
            _cashRegisters = new List<CashRegister>(FindObjectsOfType<CashRegister>());
            _shelves = new List<Shelf>(FindObjectsOfType<Shelf>());
        }

        [Button]
        private void Start()
        {
            Vector3 position = transform.position;
            GridNode currentGridNode = pathFinding.GetGrid().GetNodeFromWorldPosition(new Vector3(position.x, 0, position.z));


            Shelf randomShelf = _shelves[Random.Range(0, _shelves.Count)];
            Vector3Int targetGridPosition = randomShelf.InteractPosition;
            GridNode targetGridNode = pathFinding.GetGrid().GetNodeFromWorldPosition(new Vector3(targetGridPosition.x, 0, targetGridPosition.z));

            pathFinding.StartPathfinding(currentGridNode, targetGridNode);
        }

        private void FixedUpdate()
        {
            if (_destinationReached) return;
            if (pathFinding == null) return;
            if (pathFinding.GetGrid() == null) return;
            if (pathFinding.Path == null || pathFinding.Path.IsEmpty()) return;
            
            if (currentIndex == -1)
            {
                Debug.LogWarning("Could not found next path node");
                return;
            }
            
            Vector3 playerPos = hipRb.gameObject.transform.position;
            var nextPathNode = pathFinding.Path[currentIndex + 1];
            var nextPos = nextPathNode.GetWorldPosition(pathFinding.GetGrid().PivotPoint);
            
            hipRb.gameObject.transform.position = Vector3.MoveTowards(playerPos, nextPos, 0.01f);
            
            //calculate the rotation angel and set hipjoint target rotation to it
            // Vector3 direction = nextPos - playerPos;
            // Quaternion lookRotation = Quaternion.LookRotation(direction);
            // hipJoint.targetRotation = lookRotation;
            //
            
            //check if close enough 
            if(Vector3.Distance(playerPos, nextPathNode.GetWorldPosition(pathFinding.GetGrid().PivotPoint)) < 0.1f)
            {
                currentIndex++;
                if (currentIndex < pathFinding.Path.Count - 1) return;
                currentIndex = -1;
                _destinationReached = true;
            }
        }
    }
}