using System;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenIndicator : StorageIndicator
{
    private int _count = 0;

    public override void UpdateIndicator(Storage storage)
    {
        if(!isActiveAndEnabled)
            return;
        if (transform.childCount < storage.StorageCapacity)
            throw new UnityException("Not enough children");
        for (int i = Math.Min(_count, storage.ItemCount); i < Math.Max(_count, storage.ItemCount); i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            transform.GetChild(i).gameObject.SetActive(!child.activeSelf);
        }

        _count = storage.ItemCount;
    }
}
