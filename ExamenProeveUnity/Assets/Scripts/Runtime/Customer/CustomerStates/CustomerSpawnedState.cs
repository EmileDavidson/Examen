namespace Runtime.Customer.CustomerStates
{
    public class CustomerSpawnedState: CustomerStateBase
    {
        public override void OnStateStart()
        {
            FinishState();
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.WalkingToEntrance;
        }

        public CustomerSpawnedState(CustomerController controller) : base(controller)
        {
        }
    }
}