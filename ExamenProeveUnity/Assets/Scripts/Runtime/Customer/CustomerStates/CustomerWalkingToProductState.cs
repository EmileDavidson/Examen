using System;
using System.Linq;
using System.Threading.Tasks;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;
using Runtime.Managers;
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
            Controller.Movement.WantsToMove = true;
            var finalEntryNodeIndex = Controller.EntryPath.Path.PathNodes.Last();

            MyGrid grid = Controller.Grid;
            var startNode = grid.GetNodeByIndex(finalEntryNodeIndex);
            var endNode = grid.GetNodeByIndex(Controller.CurrentTargetShelf.InteractionGridIndex);            
            
            Controller.Movement.onDestinationReached.AddListener(FinishState);
            Controller.AStarPathFinding.FindPath(startNode, endNode, (path) =>
            {
                Controller.Movement.Path = path.Copy();
            }, () =>
            {
                //todo: this shouldn't happen for now but it might happen in the future so we should handle it
                //todo by trying to find a path every .. seconds or something like that
                Debug.Log("failed to find path"); 
            });
        }

        public override void FinishState()
        {
            Controller.Grid.GetNodeByIndex(Controller.Movement.Path.PathNodes.Last()).SetTempBlock(false, Controller.ID);
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