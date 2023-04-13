using Runtime.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Product", order = 1)]
public class ProductScriptableObject : ScriptableObject
{
    public ProductType type = ProductType.Unknown;
    public GameObject prefab;
    public Sprite icon; 
    
    public int price;
}
