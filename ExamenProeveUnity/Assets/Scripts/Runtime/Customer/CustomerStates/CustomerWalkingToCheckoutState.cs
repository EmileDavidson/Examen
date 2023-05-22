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
            Controller.Movement.WantsToMove = true;
            MyGrid grid = Controller.Grid;
            var endNodeIndex = Controller.ExitPath.Path.PathNodes.First();
            var startNode = grid.GetNodeFromWorldPosition(Controller.Hip.transform.position);
            var endNode = grid.GetNodeByIndex(endNodeIndex);
            
            Controller.Movement.onDestinationReached.AddListener(FinishState);
            Controller.aStarPathFinding.FindPath(startNode, endNode, (path) =>
            {
                Controller.Movement.Path = path.Copy();
            }, ()=>{});
        }

        public override void FinishState()
        {
            Controller.Movement.Path = null;
            Controller.Movement.onDestinationReached.RemoveListener(FinishState);
            Controller.State = CustomerState.DroppingProducts;
        }
    }
}