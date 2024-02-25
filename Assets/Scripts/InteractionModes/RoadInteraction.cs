using BuildingTools;
using Unity.Mathematics;
using System.Collections.Generic;
public class RoadInteraction : InteractionMode
{
    List<int2> CurrentRoadStroke;

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        CurrentRoadStroke = new();
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None)
        {
            PlaceTile(tile, BuildingType.Road);
            gameManager.UpdateRoad(tile);
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
            gameManager.CreateUnitSelectionPanel(tile);
        }
    }

}
