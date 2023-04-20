using System.Linq;
using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerWalkingToExitState: CustomerStateBase
    {
        public CustomerWalkingToExitState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
            Controller.Movement.onDestinationReached.AddListener(FinishState);
            Controller.Movement.Path = Controller.ExitPath.Path.Copy();
        }

        public override void FinishState()
        {
            Controller.Movement.onDestinationReached.RemoveListener(FinishState);
            Controller.Grid.GetNodeByIndex(Controller.ExitPath.Path.PathNodes.Last()).IsTempBlocked = false;
            Object.Destroy(Controller.gameObject);
        }
    }
}