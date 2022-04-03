using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using System.IO;
using UnityEditor.AssetImporters;

[ScriptedImporter( 1, "xraw" )]
public class XrawImporter : ScriptedImporter {
    public override void OnImportAsset( AssetImportContext ctx )
    {
        byte[] bytes = File.ReadAllBytes(ctx.assetPath);
        int sizeX = BitConverter.ToInt32(bytes, 8);
        int sizeY = BitConverter.ToInt32(bytes, 12);
        int sizeZ = BitConverter.ToInt32(bytes, 16);
        byte[,,] voxels = new byte[sizeX, sizeY, sizeZ];
        for(int x = 0; x < sizeX; x++)
            for(int y = 0; y < sizeY; y++)
                for(int z = 0; z < sizeZ; z++)
                    voxels[x, y, z] = bytes[20 + x + y * sizeX + z * sizeX * sizeY];
        Xraw xraw = new Xraw(voxels);
        ctx.AddObjectToAsset("xraw", xraw);
        ctx.SetMainObject(xraw);
    }
}