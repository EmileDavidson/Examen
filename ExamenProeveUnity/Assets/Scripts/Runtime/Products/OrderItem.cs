using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime
{
    public class OrderItem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;
        
        private ProductScriptableObject _product;

        public OrderItem(ProductScriptableObject type)
        {
            _product = type;
        }

        public ProductScriptableObject Product
        {
            get => _product;
            set
            {
                _product = value;

                if (image is not null)
                {
                    image.sprite = _product.Icon;
                }
            }
        }


        public void SetIcon(Sprite spriteVal)
        {
            if (image is null) return;
            image.sprite = spriteVal;
        }
        public void SetText(string val)
        {
            if (text is null) return;
            text.text = val;
        }
    }
}