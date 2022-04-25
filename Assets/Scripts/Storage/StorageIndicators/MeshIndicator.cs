using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshGenerator))]
public class MeshIndicator : StorageIndicator
{
    [SerializeField] private int _totalStages = 3;
    [SerializeField] private float _roundingOffset = 0.4f;
    private int _lastStage = -1;
    private int _totalCapacity = 0;
    private MeshGenerator _meshGenerator;
    private HashSet<Storage> _registeredStorages;

    private void OnValidate()
    {
        if (GetComponent<MeshGenerator>() == null)
            throw new UnityException("No Mesh Generator");
    }

    private void Awake()
    {
        _meshGenerator = GetComponent<MeshGenerator>();
        _registeredStorages = new HashSet<Storage>();
    }

    public override void UpdateIndicator(Storage storage)
    {
        if (!_registeredStorages.Contains(storage))
        {
            _totalCapacity += storage.StorageCapacity;
            _registeredStorages.Add(storage);
        }

        int itemCount = 0;
        foreach (Storage registeredStorage in _registeredStorages)
            itemCount += registeredStorage.ItemCount;
        int currentStage = Math.Max(0, Mathf.RoundToInt(_totalStages * 1f * itemCount / _totalCapacity + _roundingOffset));
        if (currentStage != _lastStage)
        {
            _meshGenerator.GenerateMesh(new Vector3Int(_meshGenerator.Dimensions.x,
                _meshGenerator.Dimensions.y * currentStage / _totalStages, _meshGenerator.Dimensions.z));
            _lastStage = currentStage;
        }
    }
}