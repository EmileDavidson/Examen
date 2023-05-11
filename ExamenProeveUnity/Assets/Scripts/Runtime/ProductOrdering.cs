using System;
using System.Collections.Generic;
using Runtime;
using Runtime.Managers;
using TMPro;
using Toolbox.Attributes;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductOrdering : MonoBehaviour
{
    [SerializeField] private Image productDisplay;
    [SerializeField] private ProductOrderList _orderList;
    [SerializeField] private Transform deliverAnchor;

    [SerializeField] private List<ProductScriptableObject> buyableProducts = new();
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text priceText;
    
    private ProductScriptableObject _selectedProduct;
    private int _playersInRange;

    private int _cycleIndex;
    public UnityEvent onBuyableProductsChanged = new();

    void Start()
    {
        var shelves = WorldManager.Instance.shelves;
        foreach (var shelf in shelves)
        {
            if (buyableProducts.Contains(shelf.Item)) continue;
            buyableProducts.Add(shelf.Item);
        }
        onBuyableProductsChanged.Invoke();

        LevelManager.Instance.onMoneyChange.AddListener(UpdateCashText);
        
        UpdateSelection();
    }

    public void NextItem()
    {
        _cycleIndex = buyableProducts.GetPossibleIndex(_cycleIndex + 1);
        UpdateSelection();
    }
    
    public void PreviousItem()
    {
        _cycleIndex = buyableProducts.GetPossibleIndex(_cycleIndex - 1);
        UpdateSelection();
    }

    [Button]
    public void OrderProduct()
    {
        LevelManager.Instance.Money -= _selectedProduct.BuyPrice;
        _orderList.AddProductToOrder(_selectedProduct.Type);
        Instantiate(buyableProducts[_cycleIndex].Prefab, deliverAnchor.transform.position, Quaternion.identity);
    }

    private void UpdateSelection()
    {
        _selectedProduct = buyableProducts[_cycleIndex];
        productDisplay.sprite = buyableProducts[_cycleIndex].Icon;
        UpdateCashText();
    }

    private void UpdateCashText()
    {
        moneyText.text = $"${LevelManager.Instance.Money}";
        priceText.text = $"${_selectedProduct.BuyPrice}";
    }

    public List<ProductScriptableObject> BuyableProducts => buyableProducts;
}
