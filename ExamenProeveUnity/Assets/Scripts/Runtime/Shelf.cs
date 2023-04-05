using System;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [Tooltip("The index of the grid node where the player should stand to interact with the shelf")]
    [SerializeField] private int gridIndex;
    private readonly List<ProductScriptableObject> _inventory = new List<ProductScriptableObject>() { Capacity = 5 };

    public int InteractionGridIndex => gridIndex;

    public void Restock(ProductScriptableObject item)
    {
        for (int i = _inventory.Count; i < _inventory.Capacity; i++)
        {
            _inventory.Add(item);
        }
    }

    public bool IsEmpty()
    {
        if (_inventory.Count >= 1)
        {
            return false;
        }

        return true;
    }

    public ProductScriptableObject GrabItem()
    {
        if (_inventory.Count >= 1)
        {
            ProductScriptableObject productToReturn = _inventory[0];
            _inventory.RemoveAt(0);
            return productToReturn;
        }

        return null;
    }
}