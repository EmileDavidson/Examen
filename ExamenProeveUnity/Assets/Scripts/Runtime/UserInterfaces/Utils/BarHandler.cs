using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Runtime.UserInterfaces.Utils
{
    public class BarHandler : MonoBehaviour
    {
        [SerializeField] private bool hideOnAwake = true;
        
        [SerializeField] private Image imageToScale;
        [SerializeField] private Image background;
        [SerializeField] private AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField, Range(0, 1)] private float scale = 0f;

        private void Awake()
        {
            if (hideOnAwake)
            {
                HideBar();
            }
        }

        private void SetScaleUI(float value)
        {
            imageToScale.transform.localScale = new Vector3(curve.Evaluate(value), 1, 1);
        }

        public void HideBar()
        {
            imageToScale.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
        }

        public void ShowBar()
        {
            imageToScale.gameObject.SetActive(true);
            background.gameObject.SetActive(true);
        }

        public float Scale
        {
            get => scale;
            set
            {
                value = Mathf.Clamp(value, 0, 1);
                ShowBar();
                SetScaleUI(value);
                scale = value;
            }
        }
    }
}