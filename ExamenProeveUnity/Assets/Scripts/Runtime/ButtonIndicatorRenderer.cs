using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIndicatorRenderer : MonoBehaviour
{
    public List<Image> uiImages = new();

    private void Awake()
    {
        ShowButtons(false);
    }

    public void ShowButtons(bool showButtons)
    {
        foreach (var uiImage in uiImages)
        {
            uiImage.enabled = showButtons;
        }
    }
}
