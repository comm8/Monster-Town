using UnityEngine;
using BuildingTools;
public class DeleteInteraction : InteractionMode
{
    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        //do nothing
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None)
        {
            if (tile.buildingType == BuildingType.Road)
            {
                RemoveRoad();
            }
            PlaceTile(tile, BuildingType.None);
            gameManager.monsters[tile.monsterID].tile = null;
            tile.monsterID = 0;
            
        }
    }
    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {
        //do nothing
    }

    public void RemoveRoad()
    {
        var adjacentTiles = BuildingUtils.GetAdjacentTiles(gameManager.SelectionGridPos);



        for (int i = 0; i < 4; i++)
        {
            var tile = adjacentTiles[i];

            if (gameManager.tileProperties[BuildingUtils.CoordsToSlotID(tile, gameManager.gridSize)].TryGetComponent(out RoadProperties road))
            {
                if (i == 0)
                {
                    road.table.right = false;
                }
                else if (i == 1)
                {
                    road.table.left = false;
                }
                else if (i == 2)
                {
                    road.table.up = false;
                }
                else if (i == 3)
                {
                    road.table.down = false;
                }

                UpdatetileRoadMaterial(road, road.table);

                road.GetComponentInChildren<TileAnimator>().playUpdateAnimation();


            }
        }
    }
}
