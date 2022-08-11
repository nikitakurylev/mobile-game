using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextIndicator : StorageIndicator
{
    private Text _text;
    private RectTransform _parentRect;
    
    private void OnValidate()
    {
        if (GetComponent<Text>() == null)
            throw new UnityException("No Text");
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
        _parentRect = transform.parent.GetComponent<RectTransform>();
    }
    
    public override void UpdateIndicator(Storage storage)
    {
        _text.text = storage.ItemCount + "/" + storage.StorageCapacity;
        LayoutRebuilder.ForceRebuildLayoutImmediate(_parentRect);
    }
}
