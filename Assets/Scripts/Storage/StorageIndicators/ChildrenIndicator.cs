using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenIndicator : StorageIndicator
{
    private int _count = 0;
    private ResourceEnum _currentType = ResourceEnum.None;
    [SerializeField] private List<Material> _materials;

    public override void UpdateIndicator(Storage storage)
    {
        if (_currentType != storage.ResourceType)
            foreach (MeshRenderer meshRenderer in transform.GetComponentsInChildren<MeshRenderer>(true))
                meshRenderer.material = _materials[(int) storage.ResourceType];      
        
        for (int i = Math.Min(_count, storage.ItemCount); i < Math.Max(_count, storage.ItemCount); i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            transform.GetChild(i).gameObject.SetActive(!child.activeSelf);
        }

        _currentType = storage.ResourceType;
        _count = storage.ItemCount;
    }
}
