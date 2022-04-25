using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextIndicator : StorageIndicator
{
    private Text _text;
    
    private void OnValidate()
    {
        if (GetComponent<Text>() == null)
            throw new UnityException("No Text");
    }

    private void Awake()
    {
        _text = GetComponent<Text>();
    }
    
    public override void UpdateIndicator(Storage storage)
    {
        _text.text = storage.ItemCount + "/" + storage.StorageCapacity;
    }
}
