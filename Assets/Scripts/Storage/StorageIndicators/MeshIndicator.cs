using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshGenerator))]
public class MeshIndicator : StorageIndicator
{
    [SerializeField] private int _totalStages = 3;
    private int _lastStage = -1;
    private MeshGenerator _meshGenerator;

    private void OnValidate()
    {
        if (GetComponent<MeshGenerator>() == null)
            throw new UnityException("No Mesh Generator");
    }

    private void Awake()
    {
        _meshGenerator = GetComponent<MeshGenerator>();
    }

    public override void UpdateIndicator(Storage storage)
    {
        int currentStage = Mathf.RoundToInt(_totalStages * 1f * storage.ItemCount / storage.StorageCapacity);
        if (currentStage != _lastStage)
        {
            _meshGenerator.GenerateMesh(new Vector3Int(_meshGenerator.Dimensions.x,
                _meshGenerator.Dimensions.y * currentStage / _totalStages, _meshGenerator.Dimensions.z));
            _lastStage = currentStage;
        }
    }
}