using Runtime.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Product", order = 1)]
public class ProductScriptableObject : ScriptableObject
{
    [field: SerializeField] public ProductType Type { get; private set; } = ProductType.Unknown;
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public int BuyPrice { get; private set; }
    [field: SerializeField] public int SellPrice { get; private set; }
    
    [field: SerializeField] private Sprite icon;

    public Sprite Icon => icon ? icon : Sprite.Create(new Texture2D( 1, 1 ), new Rect( 0, 0, 1, 1 ), Vector2.zero);
}