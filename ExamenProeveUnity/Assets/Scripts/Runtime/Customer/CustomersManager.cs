using System.Collections.Generic;
using Runtime.Managers;
using Toolbox.Attributes;
using Toolbox.Utils.Runtime;
using UnityEngine;
using UnityEngine.Events;
using Utilities.MethodExtensions;
using Utilities.Other.Runtime;

namespace Runtime.Customer
{
    /// <summary>
    /// Manages all customers and is the main entry point for customer related things.
    /// </summary>
    public class CustomersManager : MonoSingleton<CustomersManager>
    {
        [SerializeField] private TimerManager spawnTimerManager;
        
        private List<CustomerController> _customers = new();

        public UnityEvent onCustomerAdded = new UnityEvent();
        public UnityEvent onCustomerRemoved = new UnityEvent();

        /// <summary>
        /// Adds a customer to the scene and to the list of customers.
        /// </summary>
        /// <param name="customerPrefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void AddCustomer(GameObject customerPrefab, Vector3 position, Quaternion rotation)
        {
            var spawnedCustomer = Instantiate(customerPrefab, position, rotation);
            var customerController = spawnedCustomer.GetComponent<CustomerController>();

            if (customerController == null)
            {
                Destroy(spawnedCustomer);
                Debug.LogWarning("Tried to spawn a customer but the given prefab does not have a CustomerController!");
                return;
            }
            
            _customers.Add(customerController);
            onCustomerAdded.Invoke();
        }

        /// <summary>
        /// Removes the customer from the scene by destroying it and removing it from the list of customers.
        /// </summary>
        /// <param name="controller"></param>
        public void RemoveCustomer(CustomerController controller)
        {
            var customer = _customers.Find(c => c == controller);
            if (customer == null) return;
            
            controller.Sprites.GetScoreFromSprite(controller.EmojiType, out int max, out int min, out int score);
            LevelManager.Instance.AddScore(score, min, max);
            
            Destroy(customer.gameObject);
            _customers.Remove(customer);
            onCustomerRemoved.Invoke();
        }
        
        /// <summary>
        /// Checks if there are customers in the store.
        /// </summary>
        public bool AreCustomersInStore()
        {
            return _customers.Count > 0;
        }
    }
}