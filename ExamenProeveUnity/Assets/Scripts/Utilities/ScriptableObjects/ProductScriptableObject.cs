using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Product", order = 1)]
public class ProductScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon; 
    
    public int price;
}
