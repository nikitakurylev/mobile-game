﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Storage))]
public class StorageTarget : HumanTarget
{
    private Storage _storage;
    private int _occupied = 0;
    
    public ResourceEnum Resource => _storage.ResourceType;
    
    private void OnValidate()
    {
        if (GetComponent<Storage>() == null)
            throw new UnityException("No Storage");
    }

    private void Awake()
    {
        _storage = GetComponent<Storage>();
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