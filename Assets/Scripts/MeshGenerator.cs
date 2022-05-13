using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private VoxelMap _voxelMap;
    private const byte BlockTypeCount = 255;
    private MeshFilter _meshFilter;
    private Dictionary<ResourceEnum, Vector3Int> frames;

    public Vector3Int Dimensions => _voxelMap.Dimensions;

    private void OnValidate()
    {
        frames = new Dictionary<ResourceEnum, Vector3Int>();
        foreach (ResourceEnum resourceEnum in Enum.GetValues(typeof(ResourceEnum)).Cast<ResourceEnum>())
        {
            frames.Add(resourceEnum, new Vector3Int());
        }
        _meshFilter = GetComponent<MeshFilter>();
        if (_meshFilter == null)
            throw new UnityException("No Mesh Filter");
        if (_voxelMap != null && ResourceIndex.Instance != null)
            GenerateMesh();
    }

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        frames = new Dictionary<ResourceEnum, Vector3Int>();
        foreach (ResourceEnum resourceEnum in Enum.GetValues(typeof(ResourceEnum)).Cast<ResourceEnum>())
        {
            frames.Add(resourceEnum, new Vector3Int());
        }
    }

    public void GenerateMesh(Vector3Int frame, ResourceEnum resourceEnum)
    {
        frames[resourceEnum] = frame;
        float at = Time.realtimeSinceStartup;
        List<int> Triangles = new List<int>();
        List<Vector3> Verticies = new List<Vector3>();
        List<Vector3> Normals = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        int[,] Faces = new int[6, 9]
        {
            {0, 1, 2, 3, 0, 1, 0, 0, 0}, //top
            {7, 6, 5, 4, 0, -1, 0, 1, 0}, //bottom
            {2, 1, 5, 6, 0, 0, 1, 1, 1}, //right
            {0, 3, 7, 4, 0, 0, -1, 1, 1}, //left
            {3, 2, 6, 7, 1, 0, 0, 1, 1}, //front
            {1, 0, 4, 5, -1, 0, 0, 1, 1} //back
        };

        for (int x = 0; x < Dimensions.x; x++)
        for (int y = 0; y < Dimensions.y; y++)
        for (int z = 0; z < Dimensions.z; z++)
        {
            Vector3[] VertPos = new Vector3[8]
            {
                new Vector3(-1, 1, -1), new Vector3(-1, 1, 1),
                new Vector3(1, 1, 1), new Vector3(1, 1, -1),
                new Vector3(-1, -1, -1), new Vector3(-1, -1, 1),
                new Vector3(1, -1, 1), new Vector3(1, -1, -1),
            };
            Vector3 offset = new Vector3(0.5f - _voxelMap.Dimensions.x / 2, 0.5f, 0.5f - _voxelMap.Dimensions.z / 2);
            const float uvPadding = 0.0001f;

            float faceSize = 1f / (BlockTypeCount + 1);

            Vector3Int currentFrame = frames[ResourceIndex.BlockToResource[_voxelMap.GetVoxel(x, y, z)]];
            
            if (_voxelMap.GetVoxelCropped(x, y, z, currentFrame) != 0)
                for (int o = 0; o < 6; o++)
                {
                    int xf = x + Faces[o, 4], yf = y + Faces[o, 5], zf = z + Faces[o, 6];
                    
                    if (_voxelMap.GetVoxelCropped(xf, yf, zf, frames[ResourceIndex.BlockToResource[_voxelMap.GetVoxel(xf, yf, zf)]]) == 0)
                        AddQuad(o, Verticies.Count, _voxelMap.GetVoxel(x, y, z), faceSize);
                }

            void AddQuad(int facenum, int v, byte blockType, float faceSize)
            {
                // Add Mesh
                for (int i = 0; i < 4; i++)
                {
                    Verticies.Add(offset + new Vector3(x, y, z) + VertPos[Faces[facenum, i]] / 2f);
                    Normals.Add(new Vector3(Faces[facenum, 4], Faces[facenum, 5], Faces[facenum, 6]));
                }

                Triangles.AddRange(new List<int>() {v, v + 1, v + 2, v, v + 2, v + 3});

                // Add uvs
                Vector2 bottomleft =
                    new Vector2((blockType - 1) * faceSize,
                        0); //new Vector2(Faces[facenum, 7], Faces[facenum, 8]) / 2f;

                uv.AddRange(new List<Vector2>()
                {
                    bottomleft + new Vector2(uvPadding, 1), bottomleft + new Vector2(faceSize - uvPadding, 1),
                    bottomleft + new Vector2(faceSize - uvPadding, 0), bottomleft + new Vector2(uvPadding, 0)
                });
            }
        }

        _meshFilter.mesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray(),
            normals = Normals.ToArray()
        };
    }

    public void GenerateMesh()
    {
        foreach (ResourceEnum resourceEnum in Enum.GetValues(typeof(ResourceEnum)).Cast<ResourceEnum>())
        {
            frames[resourceEnum] = Dimensions;
        }
        GenerateMesh(Dimensions, ResourceEnum.None);
    }
}