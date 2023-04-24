using System.Collections.Generic;
using Toolbox.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class BuyProduct : MonoBehaviour
{
    [SerializeField] private Image productDisplay;
    [SerializeField] private List<ProductScriptableObject> buyableProducts;
    
    private ProductScriptableObject _selectedProduct;
    private int _playersInRange;

    private int _cycleIndex;

    void Start()
    {
        productDisplay.sprite = buyableProducts[_cycleIndex].Icon;
    }

    public void NextItem()
    {
        _cycleIndex = _cycleIndex + 1 > buyableProducts.Count - 1 ? 0 : _cycleIndex + 1;
        UpdateSelection();
    }
    
    public void PreviousItem()
    {
        _cycleIndex = _cycleIndex - 1 < 0 ? buyableProducts.Count - 1 : _cycleIndex - 1 ;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        _selectedProduct = buyableProducts[_cycleIndex];
        productDisplay.sprite = buyableProducts[_cycleIndex].Icon;
    }
}
