using System.Linq;
using Runtime.Grid;
using Runtime.Grid.GridPathFinding;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerWalkingToCheckoutState: CustomerStateBase
    {
        public CustomerWalkingToCheckoutState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
            MyGrid grid = Controller.Grid;
            var endNodeIndex = Controller.ExitPath.Path.PathNodes.First();
            var startNode = grid.GetNodeFromWorldPosition(Controller.PlayerHip.transform.position);
            
            Controller.Movement.onDestinationReached.AddListener(FinishState);
            Controller.PathFinding.StartPathfinding(startNode, grid.GetNodeByIndex(endNodeIndex), (path) =>
            {
                Controller.Movement.Path = path.Copy();
            });
            Controller.PathFinding.onNewPathFound.AddListener(PathUpdate);
            Controller.PathFinding.onFindingNewPathFailed.AddListener(PathFindingFailed);
            Controller.PathFinding.onFindingNewPath.AddListener(IsFindingNewPath);
        }

        public override void FinishState()
        {
            Controller.PathFinding.onNewPathFound.RemoveListener(PathUpdate);
            Controller.PathFinding.onFindingNewPathFailed.RemoveListener(PathFindingFailed);
            Controller.PathFinding.onFindingNewPath.RemoveListener(IsFindingNewPath);
            
            Controller.PathFinding.EndPathFinding();

            Controller.Movement.Path = null;
            Controller.Movement.onDestinationReached.RemoveListener(FinishState);
            Controller.State = CustomerState.DroppingProducts;
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