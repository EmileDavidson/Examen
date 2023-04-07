using System.Linq;

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
            Controller.Movement.Path = Controller.TargetCashRegister.ExitPath.Path.Copy();
        }

        public override void FinishState()
        {
            Controller.Movement.onDestinationReached.RemoveListener(FinishState);
            Controller.Grid.GetNodeByIndex(Controller.TargetCashRegister.ExitPath.Path.PathNodes.Last()).IsTempBlocked = false;
            Controller.DestroyThisCustomer();
        }
    }
}