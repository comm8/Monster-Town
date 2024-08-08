using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : HasBuilding, HasTile
{
    TileData tileData;
    BuildingData buildingData;


    public BuildingData GetBuilding()
    {
       return buildingData;
    }

    public TileData GetTile()
    {
     return tileData;
    }
}
