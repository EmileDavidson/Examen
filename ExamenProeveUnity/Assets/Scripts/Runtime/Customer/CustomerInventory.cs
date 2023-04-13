using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Customer
{
    /// <summary>
    /// The customer inventory is a list of items that the customer wants to buy. and that what it already has in its inventory.
    /// </summary>
    public class CustomerInventory : MonoBehaviour
    {
        private readonly List<ProductScriptableObject> _items = new List<ProductScriptableObject>() {};
     
        public List<ProductScriptableObject> Items => _items;

        public void AddItem(ProductScriptableObject item)
        {
            if (item is null) return;
            _items.Add(item);
        }
    
        public void RemoveItem(int index)
        {
            _items.RemoveAt(index);
        }

        public void RemoveAll()
        {
            _items.Clear();
        }
    }
}
