using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Vector2Int interactPosition;
    [SerializeField] private List<ProductScriptableObject> inventory = new List<ProductScriptableObject>() {Capacity = 5};
    
    public Vector2Int InteractPosition => interactPosition;
    
    public void Restock(ProductScriptableObject item)
    {
        for (int i = inventory.Count; i < inventory.Capacity; i++)
        {
            inventory.Add(item);
        }
    }

    public bool IsEmpty()
    {
        if (inventory.Count >= 1)
        {
            return false;
        }

        return true;
    }

    public ProductScriptableObject GrabItem()
    {
        if (inventory.Count >= 1)
        {
            ProductScriptableObject productToReturn = inventory[0];
            inventory.RemoveAt(0);
            return productToReturn;
        }

        return null;
    }
}
