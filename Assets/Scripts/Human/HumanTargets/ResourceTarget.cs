using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceTarget : HumanTarget
{
    [SerializeField] private Storage _storage;
    [SerializeField] private GameObject _dropPrefab;
    private int _occupied = 0;

    public ResourceEnum Resource => _storage.ResourceType;

    private void Awake()
    {
        if (_storage == null)
            throw new UnityException("No Storage Assigned");
        if (_dropPrefab == null)
            throw new UnityException("No Drop Prefab Assigned");
    }

    private void Start()
    {
        _storage.ItemCount = _storage.StorageCapacity;
    }

    public void Harvest()
    {
        _storage.ItemCount--;
        _occupied--;
        if (_occupied < 0)
            throw new UnityException("Harvested more than occupied");
        Instantiate(_dropPrefab,
            transform.position + new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f)).normalized * 3,
            Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up));
    }

    public void Occupy(int count)
    {
        if (count > GetAvailableResources())
            throw new UnityException("Trying to occupy more than available");
        _occupied += count;
    }

    public int GetAvailableResources()
    {
        return _storage.ItemCount - _occupied;
    }
}