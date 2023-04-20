using System.Linq;
using System.Threading.Tasks;
using Runtime.Managers;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public class CustomerFinishedShoppingState: CustomerStateBase
    {

        public CustomerFinishedShoppingState(CustomerController controller) : base(controller)
        {
        }

        public override async void OnStateStart()
        {
            int totalCost = 0;

            Controller.Inventory.Items.ForEach(item => totalCost += item.Price);
            LevelManager.Instance.Money += totalCost;
            
            FinishState();
        }

        public override void FinishState()
        {
            Controller.State = CustomerState.WalkingToExit;
        }
    }
}