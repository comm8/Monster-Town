using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using System.Collections.Generic;
public class RoadInteraction : InteractionMode
{
    [SerializeField] List<int2> CurrentRoadStroke = new();
    [SerializeField] SelectionScheme scheme;

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        CurrentRoadStroke.Clear();
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None && Inventory.TryChargeCost(selected, true))
        {
            if (!(CurrentRoadStroke.Count == 0 || checkAdjacent(CurrentRoadStroke[CurrentRoadStroke.Count - 1], gameManager.SelectionGridPos)))
            {
                CurrentRoadStroke.Clear();
                //tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
                return;
            }
            PlaceTile(tile, BuildingType.Road);
            UpdateRoadInStroke(tile);
        }
        else
        {
            if (tile.buildingType == BuildingType.Road)
            {
                UpdateRoadInStroke(tile);
            }
            //tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }

    }
    public override void OnPressStart(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road && !tile.locked)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public bool checkAdjacent(int2 point1, int2 point2)
    {
        if (CurrentRoadStroke.Count == 0) { return true; }
        int dx = Mathf.Abs(point1.x - point2.x);
        int dy = Mathf.Abs(point1.y - point2.y);

        if ((dx == 1 && dy == 0) || (dx == 0 && dy == 1))
        {
            return true;
        }
        return false;
    }

    public void UpdateRoadInStroke(TileProperties tile)
    {
        if (!tile.gameObject.TryGetComponent(out RoadProperties road))
        {
            road = tile.gameObject.AddComponent<RoadProperties>();
        }
        var roadTable = road.table;

        RoadTable inverseRoadTable = new();

        if (CurrentRoadStroke.Count > 0)
        {
            if (CurrentRoadStroke[^1].Equals(gameManager.SelectionGridPos))
            {
                //PlaceTile(tile, BuildingType.None);
                CurrentRoadStroke.Remove(CurrentRoadStroke.Count - 1);
                return;
            }
        }

        CurrentRoadStroke.Add(gameManager.SelectionGridPos);


        if (CurrentRoadStroke.Count > 1)
        {
            int2 previousRoad = CurrentRoadStroke[^2];

            if (new Vector2(gameManager.SelectionGridPos.x - previousRoad.x, gameManager.SelectionGridPos.y - previousRoad.y).magnitude > 1)
            {
                //Debug.Log(new Vector2(previousRoad.x, previousRoad.y).magnitude);
                return;
            }

            if (gameManager.SelectionGridPos.x > previousRoad.x)
            {
                roadTable.right = true;
                inverseRoadTable.left = true;
            }
            else if (gameManager.SelectionGridPos.x < previousRoad.x)
            {
                roadTable.left = true;
                inverseRoadTable.right = true;
            }
            else if (gameManager.SelectionGridPos.y > previousRoad.y)
            {
                roadTable.up = true;
                inverseRoadTable.down = true;
            }
            else
            {
                roadTable.down = true;
                inverseRoadTable.up = true;
            }

            UpdatetileRoadMaterial(road, roadTable);


            //adjust last road
            road = gameManager.tileProperties[BuildingUtils.CoordsToSlotID(CurrentRoadStroke[^2], gameManager.gridSize)].GetComponent<RoadProperties>();
            roadTable = road.table;

            if (roadTable.up || inverseRoadTable.up) { roadTable.up = true; }
            if (roadTable.down || inverseRoadTable.down) { roadTable.down = true; }
            if (roadTable.left || inverseRoadTable.left) { roadTable.left = true; }
            if (roadTable.right || inverseRoadTable.right) { roadTable.right = true; }

            UpdatetileRoadMaterial(road, roadTable);
        }
    }

    public override void OnTileEnter(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnTileExit(TileProperties tile, BuildingType selected)
    {

    }

    public override void OnModeEnter(TileProperties tile, BuildingType selected)
    {
        gameManager.SetSelectionScheme(scheme);
    }

    public override void OnModeExit(TileProperties tile, BuildingType selected)
    {
    }

}
