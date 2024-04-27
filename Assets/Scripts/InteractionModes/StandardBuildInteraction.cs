using BuildingTools;
using UnityEngine;
public class StandardBuildInteraction : InteractionMode
{
    [SerializeField] SelectionScheme PlaceScheme;
    [SerializeField] SelectionScheme InteractScheme;

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        //Do nothing
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None && Inventory.TryChargeCost(selected))
        {
            PlaceTile(tile, selected);
            tile.UpdateModel();
            tile.UpdateMonsterEmployment();
            CheckScheme(tile);
        }
        else
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }

    }
    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public override void OnTileEnter(TileProperties tile, BuildingType selected)
    {
        CheckScheme(tile);
    }

    public override void OnModeEnter(TileProperties tile, BuildingType selected)
    {
        gameManager.SetSelectionScheme(PlaceScheme);
    }

    public override void OnModeExit(TileProperties tile, BuildingType selected)
    {
    }


    void CheckScheme(TileProperties tile)
    {
        if (tile.buildingType == BuildingType.None)
        {
            gameManager.SetSelectionScheme(PlaceScheme);
        }
        else
        {
            gameManager.SetSelectionScheme(InteractScheme);
        }
    }
}
