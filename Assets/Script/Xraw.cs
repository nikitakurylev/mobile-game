using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xraw : Object
{
    private byte[,,] _voxels;
    private Vector3Int _dimensions;

    public Vector3Int Dimensions => _dimensions;
    public byte[,,] Voxels => _voxels;

    public Xraw(byte[,,] voxels)
    {
        _voxels = voxels;
        _dimensions = new Vector3Int(_voxels.GetLength(0), _voxels.GetLength(1), _voxels.GetLength(2));
    }

    public byte GetVoxel(int x, int y, int z)
    {
        if (x < 0 || y < 0 || z < 0 || x >= _dimensions.x || y >= _dimensions.y || z >= _dimensions.z)
            return 0;
        return _voxels[x, y, z];
    }
}
