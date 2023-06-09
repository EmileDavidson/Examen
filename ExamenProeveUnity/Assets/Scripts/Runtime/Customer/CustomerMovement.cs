﻿using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Events;
using Utilities.MethodExtensions;

namespace Runtime.Customer
{
    public class CustomerMovement : MonoBehaviour
    {
        [SerializeField] private CustomerController controller;

        [SerializeField] private MyGrid grid;
        [SerializeField] private Rigidbody hipRb;
        [SerializeField] private ConfigurableJoint hipJoint;
        [SerializeField] private Animator targetAnimator;

        public UnityEvent onDestinationReached = new UnityEvent();

        private Path _path;

        private static readonly int Walk = Animator.StringToHash("Walk");
        private bool _walk;
        private bool _canMove = true;
        private bool _wantsToMove = true;

        private readonly HashSet<int> _blockingPoints = new();

        public Path Path => _path;

        public void SetPath(Path value, bool blockFinalNode = false)
        {
            if (Equals(_path, value)) return;

            foreach (var blockingPoint in _blockingPoints)
            {
                if (blockFinalNode && blockingPoint == value.PathNodes.Last()) continue;
                grid.GetNodeByIndex(blockingPoint).SetTempBlock(false, controller.ID);
            }

            _blockingPoints.Clear();
            _path = value;
        }

        private void OnDisable()
        {
            foreach (var blockingPoint in _blockingPoints)
            {
                grid.GetNodeByIndex(blockingPoint).SetTempBlock(false, controller.ID);
            }

            _blockingPoints.Clear();
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            grid ??= WorldManager.Instance.worldGrid;
            controller ??= GetComponent<CustomerController>();
        }

        /// <summary>
        /// FixedUpdate is called once per physics frame
        /// </summary>
        private void FixedUpdate()
        {
            if (!_canMove)
            {
                _walk = false;
                targetAnimator.SetBool(Walk, _walk);
                return;
            }

            var pathExists = Path?.PathNodes != null && !Path.PathNodes.IsEmpty() && Path.CurrentIndex != -1;
            if (!pathExists || Path.GetNextNodeIndex() == -1)
            {
                StopMovement();
                return;
            }

            var isTempBlocked = (grid.GetNodeByIndex(Path.GetNextNodeIndex())?.IsTempBlocked) ?? false;
            var isBlockedByMe = (grid.GetNodeByIndex(Path.GetNextNodeIndex())?.IsBlockedBy(controller.ID)) ?? false;

            if (isTempBlocked && !isBlockedByMe)
            {
                StopMovement();
                return;
            }

            Vector3 playerPos = hipRb.gameObject.transform.position;
            int nextPathNodeIndex = Path.GetNextNodeIndex();
            var nextPos = grid.GetWorldPositionOfNode(grid.GetNodeByIndex(nextPathNodeIndex).GridPosition);

            grid.GetNodeByIndex(_path.CurrentIndex).SetTempBlock(true, controller.ID);
            grid.GetNodeByIndex(nextPathNodeIndex).SetTempBlock(true, controller.ID);

            _blockingPoints.Add(_path.CurrentIndex);
            _blockingPoints.Add(nextPathNodeIndex);

            nextPos.y = playerPos.y;

            targetAnimator.SetBool(Walk, _walk);
            ConstrainMovement(!_walk);

            //handle walk 
            var gotoPosition = Vector3.MoveTowards(playerPos, nextPos, 0.1f);
            var direction = (gotoPosition - playerPos).normalized;
            _walk = (direction.magnitude >= 0.1f);

            //handle rotation
            var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            hipRb.gameObject.transform.position = Vector3.MoveTowards(playerPos, nextPos, 0.085f);

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
            if (!(Vector3.Distance(playerPos,
                    grid.GetWorldPositionOfNode(grid.GetNodeByIndex(nextPathNodeIndex).GridPosition)) < .4f))
            {
                return;
            }

            foreach (var blockedIndex in _blockingPoints)
            {
                grid.GetNodeByIndex(blockedIndex).SetTempBlock(false, controller.ID);
            }

            grid.GetNodeByIndex(nextPathNodeIndex).SetTempBlock(true, controller.ID);
            grid.GetNodeByIndex(_path.CurrentIndex).SetTempBlock(true, controller.ID);

            _blockingPoints.Add(nextPathNodeIndex);
            _blockingPoints.Add(Path.PathNodes[Path.CurrentIndex]);

            Path.CurrentIndex++;
            if (Path.CurrentIndex < Path.PathNodes.Count - 1) return;

            Path.CurrentIndex = -1;
            Path.DestinationReached = true;
            onDestinationReached.Invoke();
        }

        /// <summary>
        /// Sets the correct restraining to the player when stopping or starting to move
        /// </summary>
        /// <param name="setConstrained"></param>
        private void ConstrainMovement(bool setConstrained)
        {
            var previousConstraints = hipRb.constraints;
            var constraints =
                setConstrained ? RigidbodyConstraints.FreezePosition : RigidbodyConstraints.FreezePositionY;

            hipRb.constraints = constraints;
            hipRb.constraints = previousConstraints;
        }

        private void StopMovement()
        {
            _walk = false;
            targetAnimator.SetBool(Walk, _walk);
            ConstrainMovement(!controller.IsGrabbed);
        }

        private void OnDrawGizmos()
        {
            if (_path == null) return;
            if (_path.PathNodes == null) return;
            if (_path.PathNodes.IsEmpty()) return;
            if (_path.CurrentIndex == -1) return;
            if (_path.GetNextNodeIndex() == -1) return;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(grid.GetWorldPositionOfNode(grid.GetNodeByIndex(_path.CurrentIndex).GridPosition), 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(grid.GetWorldPositionOfNode(grid.GetNodeByIndex(_path.GetNextNodeIndex()).GridPosition),
                0.2f);
            Gizmos.color = Color.blue;
            foreach (var pathNode in _path.PathNodes)
            {
                Gizmos.DrawSphere(grid.GetWorldPositionOfNode(grid.GetNodeByIndex(pathNode).GridPosition), 0.2f);
            }

            Gizmos.color = Color.yellow;
            foreach (var blockedPoint in _blockingPoints)
            {
                Gizmos.DrawSphere(grid.GetWorldPositionOfNode(grid.GetNodeByIndex(blockedPoint).GridPosition), 0.2f);
            }
        }

        public bool CanMove
        {
            get => _canMove;
            set => _canMove = value;
        }

        public bool WantsToMove
        {
            get => _wantsToMove;
            set => _wantsToMove = value;
        }

        public HashSet<int> BlockingPoints => _blockingPoints;
    }
}