using System.Collections;
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

    public ProductScriptableObject GrabItem()
    {
        if (inventory.Count >= 1)
        {
            return inventory[0];
        }

        return null;
    }
}
