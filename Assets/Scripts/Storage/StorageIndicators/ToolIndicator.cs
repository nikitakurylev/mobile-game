using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ToolIndicator : StorageIndicator
{
    private MeshRenderer _meshRenderer;
    [SerializeField] private List<Material> _materials;

    void OnValidate()
    {
        if (GetComponent<MeshRenderer>() == null)
            throw new UnityException("No Mesh Renderer");
    }

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void UpdateIndicator(Storage storage)
    {
        _meshRenderer.material = _materials[(int) storage.ResourceType];
    }
}