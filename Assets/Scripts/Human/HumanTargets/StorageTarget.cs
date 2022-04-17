using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StorageTarget : HumanTarget
{
    [SerializeField] private Storage _storage;
    private int _occupied = 0;
    
    public ResourceEnum Resource => _storage.ResourceType;
    
    private void Awake()
    {
        if (_storage == null)
            throw new UnityException("No Storage assigned");
    }

    public void Occupy(int count)
    {
        if (count > GetFreeSpace())
            throw new UnityException("Trying to occupy more than available");
        _occupied += count;
    }

    public void Store(int count)
    {
        _occupied -= count;
        if (_occupied < 0)
            throw new UnityException("Stored more than occupied");
        _storage.ItemCount += count;
    }

    public int GetFreeSpace()
    {
        return _storage.StorageCapacity - _storage.ItemCount - _occupied;
    }
}