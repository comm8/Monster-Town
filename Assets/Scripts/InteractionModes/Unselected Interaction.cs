using BuildingTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnselectedInteraction : InteractionMode
{
    [SerializeField] SelectionScheme NoActionScheme;
    [SerializeField] SelectionScheme InteractScheme;
    public override void OnModeEnter(BuildingData tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }

    public override void OnModeExit(BuildingData tile, BuildingType selected)
    {

    }

    public override void OnPress(BuildingData tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None)
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }
    }

    public override void OnPressEnd(BuildingData tile, BuildingType selected)
    {

    }

    public override void OnPressStart(BuildingData tile, BuildingType selected)
    {

        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road && tile.locked == false)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public override void OnTileEnter(BuildingData tile, BuildingType selected)
    {
        CheckScheme(tile, selected);
    }

    public override void OnTileExit(BuildingData tile, BuildingType selected)
    {

    }

    void CheckScheme(BuildingData tile, BuildingType selected)
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
