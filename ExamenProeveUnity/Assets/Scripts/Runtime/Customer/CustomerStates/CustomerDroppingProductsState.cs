using System.Linq;
using System.Threading.Tasks;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerDroppingProductsState : CustomerStateBase
    {
        private const int WaitTime = 4000;
        
        public CustomerDroppingProductsState(CustomerController controller) : base(controller)
        {
        }

        //todo: currently just waits 4 seconds but should drop an item on the cash register drop spot if the previous item was scanned.
        //todo: but this is not implemented yet.
        
        public override async void OnStateStart()
        {
            int cashRegisterNodeIndex = Controller.ExitPath.pathNodeIndexes.First();
            Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(true);
            await Task.Delay(WaitTime);
            Controller.Grid.GetNodeByIndex(cashRegisterNodeIndex).SetTempBlock(false);
            
            FinishState();
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.FinishingShopping;
        }
    }
}