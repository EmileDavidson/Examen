using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Products
{
    public class Box : MonoBehaviour
    {
        [SerializeField] private ProductScriptableObject content;
        [SerializeField] private int itemCount = 1;

        [SerializeField] private List<Image> images;
        [SerializeField] private List<TMP_Text> countTexts;

        private Grabbable _grabbable;
        private Interactable _interactable;

        private void Awake()
        {
            _grabbable ??= gameObject.GetComponent<Grabbable>();
            _interactable = gameObject.GetComponent<Interactable>();
            _interactable.onInteractionClicked.AddListener(OpenBox);
            
            if (content == null) return;
            
            UpdateUI();
        }

        public void OpenBox()
        {
            if (!_grabbable.IsGrabbed) return;
            
            for (int i = 0; i < itemCount; i++)
            {
                Instantiate(content.Prefab, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }

        private void UpdateUI()
        {
            UpdateImages();
            UpdateText();
        }

        private void UpdateImages()
        {
            foreach (var img in images)
            {
                img.sprite = content.Icon;
            }
        }

        private void UpdateText()
        {
            foreach (var txt in countTexts)
            {
                txt.text = $"x{itemCount}";
            }
        }

        public ProductScriptableObject Content
        {
            get => content;
            set
            {
                content = value;
                UpdateUI();
            }
        }

        public int ItemCount
        {
            get => itemCount;
            set
            {
                itemCount = value;
                UpdateUI();
            }
        }
    }
}
