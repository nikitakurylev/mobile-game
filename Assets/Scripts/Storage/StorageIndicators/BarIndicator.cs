using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BarIndicator : StorageIndicator
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public override void UpdateIndicator(Storage storage)
    {
        _image.fillAmount = storage.ItemCount * 1f / storage.StorageCapacity;
    }
}
