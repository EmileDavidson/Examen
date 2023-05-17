using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime
{
    [RequireComponent(typeof(ProductOrderList))]
    public class ProductOrderListRenderer : MonoBehaviour
    {
        [SerializeField] private GameObject listItemContainer;
        [SerializeField] private GameObject listItemPrefab;
        [SerializeField] private List<OrderItem> listItems;

        private ProductOrderList _orderList;

        private void Awake()
        {
            _orderList = GetComponent<ProductOrderList>();
            if (!listItemPrefab.TryGetComponent<OrderItem>(out _))
            {
                Debug.LogWarning("Prefab doesn't work since is does not have a OrderItem component");
                return;
            }

            _orderList.onProductsAdded.AddListener(UpdateItemInformation);
        }

        private void UpdateItemInformation()
        {
            _orderList = GetComponent<ProductOrderList>();
            _orderList.onOrderListChanged.AddListener(LoadData);

            foreach (var orderListProduct in _orderList.Products)
            {
                if (listItems.Any(element => element.Product.Type == orderListProduct.Key.Type))
                {
                    continue;
                }

                var itemObject = Instantiate(listItemPrefab, listItemContainer.transform);
                itemObject.SetActive(true);
                OrderItem orderItem = itemObject.GetComponent<OrderItem>();
                orderItem.Product = orderListProduct.Key;
                orderItem.SetIcon(orderListProduct.Key.Icon);
                orderItem.SetText(orderListProduct.Key.Type + ": x" + orderListProduct.Value);

                listItems.Add(orderItem);
            }

            LoadData();
        }

        private void LoadData()
        {
            foreach (var orderListProduct in _orderList.Products)
            {
                int amount = orderListProduct.Value;
                var orderItem = listItems.Find(element => element.Product.Type == orderListProduct.Key.Type);
                if (orderItem is null) continue;

                if (amount <= 0)
                {
                    orderItem.gameObject.SetActive(false);
                    continue;
                }

                orderItem.gameObject.SetActive(true);
                orderItem.SetText(orderListProduct.Key.Type + ": x" + orderListProduct.Value);
            }
        }
    }
}