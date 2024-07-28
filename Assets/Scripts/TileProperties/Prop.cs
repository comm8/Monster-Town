using System.Collections;
using System.Collections.Generic;
using BuildingTools;
using UnityEngine;

public class Prop : Tile
{
    ushort ID;

    public ushort GetID()
    {
       return ID;
    }

    TileType Tile.GetType()
    {
        return TileType.Prop;
    }
}
