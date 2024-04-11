using BuildingTools;
public class StandardBuildInteraction : InteractionMode
{
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
        }
        else
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }

    }
    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {
        if( tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }
}
