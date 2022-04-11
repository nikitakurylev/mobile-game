using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelMap : ScriptableObject
{
    [SerializeField] private byte[] _voxels;
    [SerializeField] private Vector3Int _dimensions;

    public Vector3Int Dimensions
    {
        get => _dimensions;
        set => _dimensions = value;
    }

    public byte[] Voxels
    {
        set => _voxels = value;
    }

    public byte GetVoxel(int x, int y, int z)
    {
        if (x < 0 || y < 0 || z < 0 || x >= _dimensions.x || y >= _dimensions.y || z >= _dimensions.z)
            return 0;
        return _voxels[x + z * Dimensions.x + y * Dimensions.x * Dimensions.z];
    }
    
    public byte GetVoxelCropped(int x, int y, int z, Vector3Int frame)
    {
        if (x >= frame.x || y >= frame.y || z >= frame.z)
            return 0;
        return GetVoxel(x, y, z);
    }
}
