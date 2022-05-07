using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshGenerator))]
public class MeshIndicator : StorageIndicator
{
    [SerializeField] private int _offset = 0;
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
        _meshGenerator.GenerateMesh(
            new Vector3Int(_meshGenerator.Dimensions.x,
                _meshGenerator.Dimensions.y * storage.ItemCount / storage.StorageCapacity + _offset, _meshGenerator.Dimensions.z),
            storage.ResourceType);
    }
}