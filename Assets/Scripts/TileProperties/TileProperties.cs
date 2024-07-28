using UnityEngine;
using BuildingTools;

public class TileProperties : GenericEntity, Tile
{
    public BuildingType buildingType;
    public GameObject model;
    public Transform modelTransform;
    public ushort monsterID = 0;
    private Renderer tileMaterial;
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

    public void SetDeletePreview(bool toggle)
    {
        UpdateModel();
        if (toggle)
        {
            tileMaterial.material.SetFloat("_DeletePreview", 1);
        }
        else
        {
            tileMaterial.material.SetFloat("_DeletePreview", 0);
        }
    }


    public void UpdateModel()
    {
        tileMaterial = GetComponentInChildren<MeshRenderer>();
    }

    public void resetscale()
    {
        model.transform.localScale = Vector3.one;
    }

    public ushort GetID()
    {
       return ID;
    }

    TileType Tile.GetType()
    {
       return TileType.Building;
    }

}


