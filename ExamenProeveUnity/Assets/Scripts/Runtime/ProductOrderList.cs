using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime
{
    public class ProductOrderList : MonoBehaviour
    {
        [SerializeField] private ProductOrdering _productOrdering;

        private Dictionary<ProductScriptableObject, int> _products = new();

        public UnityEvent onOrderListChanged = new();
        public UnityEvent onProductsAdded = new();

        private void Awake()
        {
            _productOrdering.onBuyableProductsChanged.AddListener(() =>
            {
                foreach (var productOrderingBuyableProduct in _productOrdering.BuyableProducts)
                {
                    if (productOrderingBuyableProduct.Type == ProductType.Unknown) continue;
                    if (_products.ContainsKey(productOrderingBuyableProduct))
                    {
                        continue;
                    }

                    _products[productOrderingBuyableProduct] = 0;
                    onProductsAdded.Invoke();
                }
            });
        }

        public void AddProductToOrder(ProductType type, int amount = 1)
        {
            var keyValuePair = _products.First(element => element.Key.Type == type);
            _products[keyValuePair.Key] += 1;
            onOrderListChanged.Invoke();
        }

        public void RemoveProductFromOrder(ProductType type, int amount = 1)
        {
            var keyValuePair = _products.First(element => element.Key.Type == type);
            if (_products[keyValuePair.Key] <= 0) return;
            _products[keyValuePair.Key] -= 1;
            onOrderListChanged.Invoke();
        }

        public Dictionary<ProductScriptableObject, int> Products => _products;
    }
}