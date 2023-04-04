using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Customer
{
    public class CustomerInventory : MonoBehaviour
    {
        public int maxInventorySize;
        private readonly List<ProductScriptableObject> _inventory = new List<ProductScriptableObject>() {};
    
        public List<ProductScriptableObject> GetInventory()
        {
            return _inventory;
        }

        public void AddItem(ProductScriptableObject item)
        {
            if (_inventory.Count >= maxInventorySize) return;
            _inventory.Add(item);
        }
    
        public void RemoveItem(int index)
        {
            _inventory.RemoveAt(index);
        }

        public void RemoveAll()
        {
            _inventory.Clear();
        }
    }
}
