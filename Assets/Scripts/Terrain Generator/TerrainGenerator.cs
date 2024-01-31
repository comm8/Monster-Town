using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
public class TerrainGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] verticies;
    int[] triangles;
    Vector2[] UVs;
    [SerializeField] Texture2D heightmap;

    int gridsize;
    // Start is called before the first frame update
    void Start()
    {
        gridsize = GameManager.instance.gridSize * 10;
        heightmap = new Texture2D(gridsize, gridsize, TextureFormat.R8, false);
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateMesh();


    }


    void GenerateMesh()
    {
        verticies = new Vector3[(gridsize + 1) * (gridsize + 1)];

        for (int i = 0, z = 0; z <= gridsize; z++)
        {
            for (int x = 0; x <= gridsize; x++)
            {
                verticies[i] = new Vector3(x,  0 , z);
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
        heightmap.Apply();
        //GetComponent<Renderer>().material.SetTexture("_BaseMap", heightmap);
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
    
    
    float generateTerrainnoise(int x, int z)
    {
        Vector2 UV = new Vector2(x ,z);
        Vector2 UVCenter = new Vector2(x%1 + 0.5f, z%1 + 0.5f);


        float distToCenterSquare = getRepeatingGradient(UV.x) * getRepeatingGradient(UV.y);
        distToCenterSquare = math.pow(distToCenterSquare, 0.6f);

        Vector2 AdjustedUV = math.lerp(UVCenter, UVCenter, distToCenterSquare);

        AdjustedUV = AdjustedUV / GameManager.instance.gridSize;

       float y = Mathf.PerlinNoise(AdjustedUV.x * 0.1f, AdjustedUV.y * 0.1f);
        if (y < 0.35) { y -= 0.1f; }

        heightmap.SetPixel(x, z, new Color(y,0,0));

        return y *30;
    }

    float getRepeatingGradient(float x)
    {
        return math.abs((x % 1) - 0.5f);
    }

}
