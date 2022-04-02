using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    public Vector3Int Dimentions;
    public float NoiseScale;
    public int blockTypeCount;
    byte[,,] Voxels;

    private void Awake()
    {
        Voxels = new byte[Dimentions.x, Dimentions.y, Dimentions.z];
        Voxels[1, 1, 3] = 3;
        Voxels[1, 1, 1] = 3;
        Voxels[1, 1, 2] = 3;
        Voxels[1, 2, 2] = 3;
        Voxels[1, 3, 2] = 3;
        Voxels[1, 4, 2] = 2;
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        float at = Time.realtimeSinceStartup;
        List<int> Triangles = new List<int>();
        List<Vector3> Verticies = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        for (int x = 1; x < Dimentions.x - 1; x++)
            for (int y = 1; y < Dimentions.y - 1; y++)
                for (int z = 1; z < Dimentions.z - 1; z++)
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

                    float faceSize = 1f / blockTypeCount;
                    if (Voxels[x, y, z] != 0)
                        for (int o = 0; o < 6; o++)
                            if (Voxels[x + Faces[o, 4], y + Faces[o, 5], z + Faces[o, 6]] == 0)
                                AddQuad(o, Verticies.Count, Voxels[x, y, z], faceSize);

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

        GetComponent<MeshFilter>().mesh = new Mesh()
        {
            vertices = Verticies.ToArray(),
            triangles = Triangles.ToArray(),
            uv = uv.ToArray()
        };

    }
}