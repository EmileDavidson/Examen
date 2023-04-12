using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Runtime.Interfaces;
using TMPro;
using Toolbox.MethodExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utilities.MethodExtensions;

namespace Runtime
{
    public class Shelf : MonoBehaviour, IGridable
    {
        [SerializeField] private int maxInventorySize = 5;
        [SerializeField] private ProductScriptableObject _item;
        [SerializeField] private List<GameObject> _displayedItems = new List<GameObject>();
        [SerializeField] private TMP_Text itemCountText;
        [SerializeField] private Canvas canvas;
    
        [Tooltip("The index of the grid node where the player should stand to interact with the shelf")]
        [field: SerializeField] 
        public int gridIndex { get; set; }
    
        public int InteractionGridIndex => gridIndex;
        public ProductScriptableObject Item => _item;
        public int ItemCount => _itemCount;
    
        private int _itemCount;
    
        public UnityEvent onItemAdded = new();
        public UnityEvent onItemRemoved = new();
        public UnityEvent onInventorySizeChange = new();


        private void Awake()
        {
            _displayedItems.ForEach(item => item.SetActive(false));
            for (int i = 0; i < _itemCount; i++)
            {
                if(!_displayedItems.ContainsSlot(i)) continue;
                _displayedItems[i].SetActive(true);
            }

            if (canvas != null)
            {
                canvas.enabled = false;
            }
            
            if(itemCountText != null)
            {
                itemCountText.text = _itemCount.ToString();
            }
        }

        /// <summary>
        /// Fulls the shelf with the given item
        /// </summary>
        public void Full()
        {
            _itemCount = maxInventorySize;
            onItemAdded.Invoke();
            onInventorySizeChange.Invoke();
            
            _displayedItems.ForEach(item => item.SetActive(true));
        }

        /// <summary>
        /// Empties the shelf
        /// </summary>
        public void Empty()
        {
            _itemCount = 0;
            onItemRemoved.Invoke();
            onInventorySizeChange.Invoke();
            
            _displayedItems.ForEach(item => item.SetActive(false));
        }

        /// <summary>
        /// Adds an item to the shelf
        /// </summary>
        public void AddItem()
        {
            if(_itemCount >= maxInventorySize) return;
            
            onItemAdded.Invoke();
            onInventorySizeChange.Invoke();

            _itemCount++;

            int slot = _itemCount + 1;
            var contains = _displayedItems.ContainsSlot(slot);
            if (contains)
            {
                _displayedItems[slot].SetActive(true);
            }
        }

        /// <summary>
        /// Removes an item from the shelf
        /// </summary>
        public void RemoveItem()
        {
            if(_itemCount <= 0) return;
            
            onItemRemoved.Invoke();
            onInventorySizeChange.Invoke();

            _itemCount--;
            
            int slot = _itemCount - 1;
            var contains = _displayedItems.ContainsSlot(slot);
            if (contains)
            {
                _displayedItems[slot].SetActive(true);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var isProduct = collision.transform.TryGetComponent<Product>(out var product);
            if (!isProduct) return;

            if (product.productScriptableObject is null || _item is null) return;
            if (product.productScriptableObject.type != _item.type) return;
            
            if(_itemCount >= maxInventorySize) return;
            
            Destroy(product.gameObject);
            AddItem();
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.transform.CompareTag("Player"))
            {
                HandlePlayerCollisionEnter();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.transform.CompareTag("Player"))
            {
                HandlePlayerCollisionExit();
            }
        }

        private void HandlePlayerCollisionExit()
        {
            if (itemCountText is null) return;
            canvas.enabled = false;
        }

        private void HandlePlayerCollisionEnter()
        {
            if (itemCountText is null) return;
            itemCountText.text = _itemCount + "/" + maxInventorySize;
            canvas.enabled = true;
        }
    }
}