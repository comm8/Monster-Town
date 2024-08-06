using BuildingTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnselectedInteraction : InteractionMode
{
    [SerializeField] SelectionScheme NoActionScheme;
    [SerializeField] SelectionScheme InteractScheme;
    public override void OnModeEnter(BuildingProperties tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }

    public override void OnModeExit(BuildingProperties tile, BuildingType selected)
    {

    }

    public override void OnPress(BuildingProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None)
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }
    }

    public override void OnPressEnd(BuildingProperties tile, BuildingType selected)
    {

    }

    public override void OnPressStart(BuildingProperties tile, BuildingType selected)
    {

        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road && tile.locked == false)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public override void OnTileEnter(BuildingProperties tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }

    public override void OnTileExit(BuildingProperties tile, BuildingType selected)
    {

    }

    void CheckScheme(BuildingProperties tile, BuildingType selected)
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
