using Runtime.Grid.GridPathFinding;
using Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Environment
{
    public class CashRegister : MonoBehaviour, IGridAble
    {
        [field: SerializeField] public int GridIndex { get; private set; }
        [SerializeField] private FixedPath exitPath;
        [SerializeField] private Transform dropOffAnchor;
        
        private GameObject _productToBeScanned;

        public UnityEvent onScanned = new();
        public UnityEvent onProductScanned = new();

        /// <summary>
        /// Spawns the product at the drop off spot of the cash register and sets it as the product to be scanned
        /// </summary>
        /// <param name="product"></param>
        public void InstantiateProduct(ProductScriptableObject product)
        {
            _productToBeScanned = Instantiate(product.Prefab, dropOffAnchor.position, Quaternion.identity);
        }

        /// <summary>
        /// On trigger enter checks if an object 
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter(Collider collision)
        {
            if (!collision.gameObject.CompareTag("Player")) onScanned.Invoke();
            if (collision.gameObject != _productToBeScanned) return;
        
            onProductScanned.Invoke();
        
            DestroyScannedObject();
        }

        /// <summary>
        /// Destroys the product to be scanned if it is not null
        /// and set the product to be scanned to null
        /// </summary>
        private void DestroyScannedObject()
        {
            if (_productToBeScanned is null) return;
        
            Destroy(_productToBeScanned.gameObject);
            _productToBeScanned = null;
        }
        
        #region getter and setter
        
        public int InteractionGridIndex => GridIndex;
        public FixedPath ExitPath => exitPath;
        
        #endregion getter and setter
    }
}
