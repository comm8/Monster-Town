using BuildingTools;
public class DeleteInteraction : InteractionMode
{
    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        //do nothing
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if(tile.buildingType != BuildingType.None)
        {
            PlaceTile(tile, BuildingType.None);
        }
    }
    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {
        //do nothing
    }
}
