using System;
using System.Collections.Generic;
using Runtime;
using Runtime.Enums;
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
    [SerializeField] private Truck truck;

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
        truck.onDoorsOpened.AddListener(DeliverProducts);
        
        UpdateSelection();
    }

    [Button]
    public void NextItem()
    {
        _cycleIndex = buyableProducts.GetPossibleIndex(_cycleIndex + 1);
        UpdateSelection();
    }
    
    
    [Button]
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
    }

    [Button]
    public void DeliverProducts()
    {
        var removedProducts = new List<Tuple<ProductType, int>>();
        foreach (var orderListProduct in _orderList.Products)
        {
            for (int i = 0; i < orderListProduct.Value; i++)
            {
                Instantiate(orderListProduct.Key.Prefab, deliverAnchor.position, Quaternion.identity);
            }
            
            removedProducts.Add(new Tuple<ProductType, int>(orderListProduct.Key.Type, orderListProduct.Value));
        }

        foreach (var tuple in removedProducts)
        {
            ProductType type = tuple.Item1;
            int amount = tuple.Item2;
            
            _orderList.RemoveProductFromOrder(type, amount);
        }
        removedProducts.Clear();
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
