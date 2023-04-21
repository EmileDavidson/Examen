﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Runtime.Managers;
using Toolbox.MethodExtensions;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerWalkingToProductState: CustomerStateBase
    {
        public CustomerWalkingToProductState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
            var finalEntryNodeIndex = Controller.EntryPath.Path.PathNodes.Last();

            MyGrid grid = Controller.Grid;
            var startNode = grid.GetNodeByIndex(finalEntryNodeIndex);
            var endNode = grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex);            
            
            Controller.Movement.onDestinationReached.AddListener(FinishState);
            Controller.PathFinding.StartPathfinding(startNode, endNode, (path) =>
            {
                Controller.Movement.Path = path.Copy();
            });
            Controller.PathFinding.onNewPathFound.AddListener(PathUpdate);
            Controller.PathFinding.onFindingNewPathFailed.AddListener(PathFindingFailed);
            Controller.PathFinding.onFindingNewPath.AddListener(IsFindingNewPath);
        }

        public override void FinishState()
        {
            Controller.Grid.GetNodeByIndex(Controller.Movement.Path.PathNodes.Last()).SetTempBlock(false, Controller.ID);
            
            Controller.PathFinding.onNewPathFound.RemoveListener(PathUpdate);
            Controller.PathFinding.onFindingNewPathFailed.RemoveListener(PathFindingFailed);
            Controller.PathFinding.onFindingNewPath.RemoveListener(IsFindingNewPath);
            
            Controller.PathFinding.EndPathFinding();
            
            Controller.Movement.onDestinationReached.RemoveListener(FinishState);
            Controller.State = CustomerState.GettingProducts;
        }
        
        private void PathUpdate(Path newPath)
        {
            Controller.Movement.Path = newPath;
        }

        private void IsFindingNewPath()
        {
            Controller.Movement.Path = null;
        }
        
        private void PathFindingFailed(Path oldPath)
        {
            Controller.Movement.Path = oldPath;
        }
    }
}