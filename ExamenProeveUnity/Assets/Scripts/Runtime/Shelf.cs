using System.Collections.Generic;
using Runtime.Interfaces;
using UnityEngine;

public class Shelf : MonoBehaviour, IGridable
{
    [Tooltip("The index of the grid node where the player should stand to interact with the shelf")]
    private readonly List<ProductScriptableObject> _inventory = new List<ProductScriptableObject>() { Capacity = 5 };
    
    [field: SerializeField] public int gridIndex { get; set; }
    public int InteractionGridIndex => gridIndex;

    public void Restock(ProductScriptableObject item)
    {
        for (int i = _inventory.Count; i < _inventory.Capacity; i++)
        {
            _inventory.Add(item);
        }
    }

    public ProductScriptableObject GrabItem()
    {
        if (_inventory.Count < 1) return null;
        ProductScriptableObject productToReturn = _inventory[0];
        _inventory.RemoveAt(0);
        return productToReturn;
    }

}