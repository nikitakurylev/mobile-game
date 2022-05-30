using System;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenIndicator : StorageIndicator
{
    private int _count = 0;

    public override void UpdateIndicator(Storage storage)
    {
        if(transform.childCount < storage.ItemCount)
            return;
        for (int i = Math.Min(_count, storage.ItemCount); i < Math.Max(_count, storage.ItemCount); i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            transform.GetChild(i).gameObject.SetActive(!child.activeSelf);
        }

        _count = storage.ItemCount;
    }
}
