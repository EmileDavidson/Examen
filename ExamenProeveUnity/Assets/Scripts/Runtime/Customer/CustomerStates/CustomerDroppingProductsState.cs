using System.Threading.Tasks;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerDroppingProductsState : CustomerStateBase
    {

        public CustomerDroppingProductsState(CustomerController controller) : base(controller)
        {
        }

        //todo: currently just waits 4 seconds but should drop an item on the cash register drop spot if the previous item was scanned.
        //todo: but this is not implemented yet.
        
        public override async void OnStateStart()
        {
            await Task.Delay(4000);
            FinishState();
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.FinishingShopping;
        }
    }
}