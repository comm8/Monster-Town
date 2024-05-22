using BuildingTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnselectedInteraction : InteractionMode
{
    [SerializeField] SelectionScheme NoActionScheme;
    [SerializeField] SelectionScheme InteractScheme;
    public override void OnModeEnter(TileProperties tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }

    public override void OnModeExit(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None)
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }
    }

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {

        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road && tile.locked == false)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public override void OnTileEnter(TileProperties tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }


    void CheckScheme(TileProperties tile, BuildingType selected)
    {
        if ((tile.buildingType != BuildingType.None) && (tile.buildingType != BuildingType.Road) && (tile.locked == false))
        {
            gameManager.SetSelectionScheme(InteractScheme);
        }
        else
        {
            gameManager.SetSelectionScheme(NoActionScheme);
        }
    }
}
