using System.Threading.Tasks;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerGettingProductState: CustomerStateBase
    {
        public override async void OnStateStart()
        {
            await Task.Delay(4000);
            FinishState();
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.WalkingToCheckout;
        }

        public CustomerGettingProductState(CustomerController controller) : base(controller)
        {
        }
    }
}