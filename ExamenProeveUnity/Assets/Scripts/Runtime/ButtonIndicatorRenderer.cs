using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicatorRenderer : MonoBehaviour
{
    [SerializeField] private List<Image> Images = new();
    [SerializeField] private bool rotateToCamera = false;

    private bool _imagesAreShown = false;
    private Transform _cameraTransform;

    private void Awake()
    {
        ShowButtons(false);
    }

    private void Update()
    {
        if (!_imagesAreShown) return;
        foreach (var uiImage in Images)
        {
            RectTransform imageTransform = uiImage.gameObject.GetComponent<RectTransform>();
            _cameraTransform ??= Camera.main.transform;

            if (!rotateToCamera) return;
            imageTransform.rotation = Quaternion.LookRotation(imageTransform.position - _cameraTransform.position, Vector3.up);
        }
    }

    public void ShowButtons(bool showButtons)
    {
        _imagesAreShown = showButtons;
        foreach (var uiImage in Images)
        {
            uiImage.enabled = showButtons;
        }
    }
}
