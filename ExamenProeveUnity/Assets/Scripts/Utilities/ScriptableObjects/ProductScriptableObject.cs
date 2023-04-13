using Runtime.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Product", order = 1)]
public class ProductScriptableObject : ScriptableObject
{
    [field: SerializeField] public ProductType Type { get; private set; } = ProductType.Unknown;
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField] public int Price { get; private set; }
}