using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private Xraw _xraw;
    private const byte BlockTypeCount = 255;
    private Vector3Int _dimensions;
    private MeshFilter _meshFilter;

    private void OnValidate()
    {
        if (GetComponent<MeshFilter>() == null)
            throw new UnityException("No Mesh Filter");
    }

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        GenerateMesh();
    }

    public void GenerateMesh()
    {
        float at = Time.realtimeSinceStartup;
        List<int> Triangles = new List<int>();
        List<Vector3> Verticies = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        for (int x = 0; x < _dimensions.x; x++)
            for (int y = 0; y < _dimensions.y; y++)
                for (int z = 0; z < _dimensions.z; z++)
                {
                    Vector3[] VertPos = new Vector3[8]{
                        new Vector3(-1, 1, -1), new Vector3(-1, 1, 1),
                        new Vector3(1, 1, 1), new Vector3(1, 1, -1),
                        new Vector3(-1, -1, -1), new Vector3(-1, -1, 1),
                        new Vector3(1, -1, 1), new Vector3(1, -1, -1),
                    };

                    int[,] Faces = new int[6, 9]{
                        {0, 1, 2, 3, 0, 1, 0, 0, 0},     //top
                        {7, 6, 5, 4, 0, -1, 0, 1, 0},   //bottom
                        {2, 1, 5, 6, 0, 0, 1, 1, 1},     //right
                        {0, 3, 7, 4, 0, 0, -1,  1, 1},   //left
                        {3, 2, 6, 7, 1, 0, 0,  1, 1},    //front
                        {1, 0, 4, 5, -1, 0, 0,  1, 1}    //back
                    };

                    float faceSize = 1f / (BlockTypeCount + 1);
                    if (_xraw.GetVoxel(x, y, z) != 0)
                        for (int o = 0; o < 6; o++)
                            if (_xraw.GetVoxel(x + Faces[o, 4], y + Faces[o, 5], z + Faces[o, 6]) == 0)
                                AddQuad(o, Verticies.Count, _xraw.GetVoxel(x, y, z), faceSize);

                    void AddQuad(int facenum, int v, byte blockType, float faceSize)
                    {
                        // Add Mesh
                        for (int i = 0; i < 4; i++) Verticies.Add(new Vector3(x, y, z) + VertPos[Faces[facenum, i]] / 2f);
                        Triangles.AddRange(new List<int>() { v, v + 1, v + 2, v, v + 2, v + 3 });

                        // Add uvs
                        Vector2 bottomleft = new Vector2((blockType - 1) * faceSize, 0);//new Vector2(Faces[facenum, 7], Faces[facenum, 8]) / 2f;

                        uv.AddRange(new List<Vector2>() { bottomleft + new Vector2(0, 1), bottomleft + new Vector2(faceSize, 1), bottomleft + new Vector2(faceSize, 0), bottomleft });
                    }
                }

        _meshFilter.mesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };

    }
}