using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerGettingProductState: CustomerStateBase
    {
        public override async void OnStateStart()
        {
            await Task.Delay(4000);
            
            var removedProduct = Controller.CurrentTargetShelf.RemoveItem();
            if(!removedProduct) FinishState();
            
            Controller.Inventory.AddItem(Controller.CurrentTargetShelf.Item);

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