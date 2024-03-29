using BuildingTools;
public class StandardBuildInteraction : InteractionMode
{
    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        //Do nothing
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None && TryChargeCost(selected))
        {
            PlaceTile(tile, selected);
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
            gameManager.CreateUnitSelectionPanel(tile);
        }
    }
}
