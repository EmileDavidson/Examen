using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Product", order = 1)]
public class ProductScriptableObject : ScriptableObject
{
    public GameObject gameObject;

    public int price;
}
