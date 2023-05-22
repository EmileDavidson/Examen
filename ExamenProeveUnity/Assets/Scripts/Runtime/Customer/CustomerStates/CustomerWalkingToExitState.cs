using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerWalkingToExitState : CustomerStateBase
    {
        public CustomerWalkingToExitState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
            Controller.Movement.WantsToMove = true;
            Controller.Movement.onDestinationReached.AddListener(FinishState);
            Controller.Movement.Path = Controller.ExitPath.Path.Copy();
        }

        public override void FinishState()
        {
            Controller.Movement.onDestinationReached.RemoveListener(FinishState);
            Controller.Grid.GetNodeByIndex(Controller.ExitPath.Path.PathNodes.Last())
                .SetTempBlock(false, Controller.ID);
            CustomersManager.Instance.RemoveCustomer(Controller);
        }
    }
}