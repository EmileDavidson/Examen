using System.Collections.Generic;
using Runtime.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utilities.MethodExtensions;

namespace Runtime.Environment
{
    public class Shelf : MonoBehaviour, IGridAble
    {
        [field: Header("Settings and References")]
        [field: SerializeField]
        public int GridIndex { get; set; }

        [SerializeField] private int maxInventorySize = 5;
        [SerializeField] private int itemCount;
        [SerializeField] private ProductScriptableObject item;
        [SerializeField] private List<GameObject> displayedItems = new List<GameObject>();

        [Header("UI")]
        [SerializeField] private TMP_Text itemCountText;
        [SerializeField] private Canvas canvas;

        [Header("Events")] [HideInInspector] public UnityEvent onItemAdded = new();
        [HideInInspector] public UnityEvent onItemRemoved = new();
        [HideInInspector] public UnityEvent onInventorySizeChange = new();

        /// <summary>
        /// Awake will setup everything for the shelf
        /// that may include setting up the UI or the inventory, displayed items etc.
        /// </summary>
        private void Awake()
        {
            HandleDisplayItemsDisplay();
            
            if (canvas != null)
            {
                canvas.enabled = false;
            }
        }


        /// <summary>
        /// Fills the shelf with the given item
        /// </summary>
        public void Fill()
        {
            itemCount = maxInventorySize;
            onItemAdded.Invoke();
            onInventorySizeChange.Invoke();

            HandleDisplayItemsDisplay();
        }

        /// <summary>
        /// Empties the shelf
        /// </summary>
        public void Empty()
        {
            itemCount = 0;
            onItemRemoved.Invoke();
            onInventorySizeChange.Invoke();

            HandleDisplayItemsDisplay();
        }

        /// <summary>
        /// Adds an item to the shelf
        /// </summary>
        private void AddItem()
        {
            if (itemCount >= maxInventorySize) return;

            onItemAdded.Invoke();
            onInventorySizeChange.Invoke();

            itemCount++;

            HandleDisplayItemsDisplay();
        }

        /// <summary>
        /// Removes item from the shelf and updates the display 
        /// </summary>
        public bool RemoveItem()
        {
            if (itemCount <= 0) return false;

            onItemRemoved.Invoke();
            onInventorySizeChange.Invoke();

            itemCount--;
            HandleDisplayItemsDisplay();

            return true;
        }

        /// <summary>
        /// When a product collides it will remove the product and add one to the counter if the
        /// shelf is already full nothing happens
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            var isProduct = collision.transform.TryGetComponent<Product>(out var product);
            if (!isProduct) return;

            if (product.productScriptableObject is null || item is null) return;
            if (product.productScriptableObject.Type != item.Type) return;

            if (itemCount >= maxInventorySize) return;

            Destroy(product.gameObject);
            AddItem();
        }

        /// <summary>
        /// OnTriggerStay enables the canvas 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            if (!other.transform.CompareTag("Player")) return;
            
            if (canvas is null) return;
            canvas.enabled = true;
        }

        /// <summary>
        /// OnTriggerExit disables the canvas since the player is no longer close enough 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (!other.transform.CompareTag("Player")) return;
            if (canvas is null) return;
            canvas.enabled = false;
        }
        
        /// <summary>
        /// Updates the item count text to the current item count and max inventory size
        /// </summary>
        private void UpdateItemCountText()
        {
            if (itemCountText is null) return;
            itemCountText.text = itemCount + "/" + maxInventorySize;
        }

        /// <summary>
        /// Disable all display items
        /// </summary>
        private void DisableAllDisplayItems()
        {
            displayedItems.ForEach(element => element.SetActive(false));
        }

        /// <summary>
        /// Enables the itemCount amount of display items 
        /// </summary>
        private void EnableDisplayItems()
        {
            for (int i = 0; i < itemCount; i++)
            {
                if (!displayedItems.ContainsSlot(i)) continue;
                displayedItems[i]?.SetActive(true);
            }
        }

        /// <summary>
        /// Updates the display (ui, and items) 
        /// </summary>
        private void HandleDisplayItemsDisplay()
        {
            DisableAllDisplayItems();
            EnableDisplayItems();
            UpdateItemCountText();
        }

        
        #region getters & setters
        
        public int InteractionGridIndex => GridIndex;
        public ProductScriptableObject Item => item;
        
        #endregion getters & setters
    }
}