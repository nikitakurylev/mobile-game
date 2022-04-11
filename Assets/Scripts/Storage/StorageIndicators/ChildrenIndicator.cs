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
        for (int i = Math.Min(_count, storage.ItemCount); i < Math.Max(_count, storage.ItemCount); i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(!child.activeSelf);
            if (_currentType != storage.ResourceType)
                child.GetComponent<MeshRenderer>().material = _materials[(int) storage.ResourceType];
        }

        _currentType = storage.ResourceType;
        _count = storage.ItemCount;
    }
}
