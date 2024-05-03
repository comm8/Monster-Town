using UnityEngine;
using BuildingTools;
using Unity.Mathematics;

public class TileProperties : GenericEntity
{
    public BuildingType buildingType;
    public GameObject model;
    public Transform modelTransform;
    public ushort monsterID = 0;
    public Renderer tileMaterial;
    public bool locked;
    public ushort ID;
    public void UpdateMonsterEmployment()
    {
        if (monsterID == 0)
        {
            tileMaterial.material.SetFloat("_Employed", 0);
        }
        else
        {
            tileMaterial.material.SetFloat("_Employed", 1);
        }
    }

    public void UpdateModel()
    {
        tileMaterial = GetComponentInChildren<MeshRenderer>();
    }



}


