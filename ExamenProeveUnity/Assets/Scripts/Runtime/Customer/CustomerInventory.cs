using System.Collections.Generic;
using UnityEngine;

public class CustomerInventory : MonoBehaviour
{
    public int maxInventorySize;
    private List<ProductScriptableObject> _inventory = new List<ProductScriptableObject>() {};

    public void AddItem(ProductScriptableObject item)
    {
        if (_inventory.Count >= maxInventorySize) return;
        _inventory.Add(item);
    }

    public List<ProductScriptableObject> GetInventory()
    {
        return _inventory;
    }

    public void RemoveItem(int index)
    {
        _inventory.RemoveAt(index);
    }

    public void RemoveItem(ProductScriptableObject item)
    {
        _inventory.Remove(item);
    }
}
