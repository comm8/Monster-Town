using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticies;
    int[] triangles;
    Vector2[] UVs;

    int gridsize;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        gridsize = GameManager.instance.gridSize * 10;
        GenerateMesh();


    }


    void GenerateMesh()
    {
        verticies = new Vector3[(gridsize + 1) * (gridsize + 1)];

        for (int i = 0, z = 0; z <= gridsize; z++)
        {
            for (int x = 0; x <= gridsize; x++)
            {
                verticies[i] = new Vector3(x,  generateTerrainnoise(x, z) , z);
                i++;
            }
        }

        triangles = new int[gridsize * gridsize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < gridsize; z++)
        {
            for (int x = 0; x < gridsize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + gridsize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + gridsize + 1;
                triangles[tris + 5] = vert + gridsize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }


        UVs = new Vector2[verticies.Length];
        for (int i = 0, z = 0; z <= gridsize; z++)
        {
            for (int x = 0; x <= gridsize; x++)
            {
                UVs[i] = new Vector2((float)x /gridsize, (float)z/gridsize);
                i++;
            }
        }


        UpdateMesh();
    }

    float generateTerrainnoise(float x, float z)
    {
        float y = 0;
        y = Mathf.PerlinNoise(x*0.05f, z* 0.05f) *5 ;
        return y;
    }

    void UpdateMesh()
    {
        if(TryGetComponent<MeshCollider>(out MeshCollider meshCollider))
        {
            Destroy(meshCollider);
        }


        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.uv = UVs;

        //mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
        meshCollider = gameObject.AddComponent<MeshCollider>();
        //meshCollider.convex = true;
        meshCollider.providesContacts = true;
    }
}
