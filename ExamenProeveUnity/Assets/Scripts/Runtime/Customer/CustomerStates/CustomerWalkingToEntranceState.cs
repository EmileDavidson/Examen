﻿namespace Runtime.Customer.CustomerStates
{
    public class CustomerWalkingToEntranceState: CustomerStateBase
    {

        public CustomerWalkingToEntranceState(CustomerController controller) : base(controller)
        {
        }

        public override void OnStateStart()
        {
            Controller.Movement.onDestinationReached.AddListener(FinishState);
            Controller.Movement.Path = Controller.EntryPath.Path.Copy();
        }

        public override void FinishState()
        {
            Controller.Movement.onDestinationReached.RemoveListener(FinishState);
            Controller.State = CustomerState.WalkingToProducts;
        }
    }
}