using UnityEngine;

namespace Runtime.Customer.CustomerStates
{
    public abstract class CustomerStateBase 
    {
        [field: SerializeField] public CustomerController Controller { get; set; }
        
        /// <summary>
        /// When the state is entered, this method is called.
        /// </summary>
        public virtual void OnStateStart(){}
        
        /// <summary>
        /// Gets called every frame while the state is active
        /// </summary>
        public virtual void OnStateUpdate(){}

        /// <summary>
        /// Should be called to finish the state.
        /// </summary>
        public abstract void FinishState();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="controller"></param>
        protected CustomerStateBase(CustomerController controller)
        {
            this.Controller = controller;
        }
    }
}