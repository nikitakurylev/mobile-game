using System;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.AssetImporters;

[ScriptedImporter( 1, "xraw" )]
public class VoxelMapImporter : ScriptedImporter {
    public override void OnImportAsset( AssetImportContext ctx )
    {
        byte[] bytes = File.ReadAllBytes(ctx.assetPath);
        int sizeX = BitConverter.ToInt32(bytes, 8);
        int sizeZ = BitConverter.ToInt32(bytes, 12);
        int sizeY = BitConverter.ToInt32(bytes, 16);
        byte[,,] voxels = new byte[sizeX, sizeY, sizeZ];
        VoxelMap voxelMap = ScriptableObject.CreateInstance<VoxelMap>();
        voxelMap.Dimensions = new Vector3Int(sizeX, sizeY, sizeZ);
        voxelMap.Voxels = bytes.Skip(24).Take(sizeX * sizeY * sizeZ).ToArray();
        ctx.AddObjectToAsset("xraw", voxelMap);
        ctx.SetMainObject(voxelMap);
    }
}
#endif
