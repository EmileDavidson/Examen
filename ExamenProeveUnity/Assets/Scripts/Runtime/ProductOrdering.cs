using System.Collections.Generic;
using Runtime.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.MethodExtensions;

namespace Runtime
{
    public class ProductOrdering : MonoBehaviour
    {
        [SerializeField] private Image productDisplay;
        [SerializeField] private Transform deliverAnchor;
        [SerializeField] private List<ProductScriptableObject> buyableProducts;

        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text priceText;
    
        private ProductScriptableObject _selectedProduct;
        private int _playersInRange;
        private int _cycleIndex;

        void Start()
        {
            LevelManager.Instance.onMoneyChange.AddListener(UpdateCashText);
        
            _selectedProduct = buyableProducts[_cycleIndex];
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

        public void OrderProduct()
        {
            LevelManager.Instance.Money -= _selectedProduct.BuyPrice;
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
    }
}
