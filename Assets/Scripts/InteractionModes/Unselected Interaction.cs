using BuildingTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnselectedInteraction : InteractionMode
{
    [SerializeField] SelectionScheme InteractScheme;
    public override void OnModeEnter(TileProperties tile, BuildingType selected)
    {
        gameManager.SetSelectionScheme(InteractScheme);
    }

    public override void OnModeExit(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnPress(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnTileEnter(TileProperties tile, BuildingType selected)
    {

    }
}
