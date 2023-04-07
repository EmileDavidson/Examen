namespace Runtime.Customer.CustomerStates
{
    public class CustomerFinishedShoppingState: CustomerStateBase
    {
        public CustomerFinishedShoppingState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
            FinishState();
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.WalkingToExit;
        }
    }
}