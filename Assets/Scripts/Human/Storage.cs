using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private ResourceEnum _resourceType = ResourceEnum.None;
    [SerializeField] private int _storageCapacity = 3;
    private int _itemCount = 0;
    
    public ResourceEnum ResourceType
    {
        get => _resourceType;
        set => _resourceType = value;
    }

    public int ItemCount
    {
        get => _itemCount;
        set => _itemCount = value;
    }

    public int StorageCapacity
    {
        get => _storageCapacity;
        set => _storageCapacity = value;
    }

    public bool HasFreeSpace()
    {
        return _itemCount < _storageCapacity;
    }
}
