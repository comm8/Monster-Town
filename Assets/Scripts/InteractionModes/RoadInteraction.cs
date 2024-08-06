using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using System.Collections.Generic;
public class RoadInteraction : InteractionMode
{
    [SerializeField] List<int3> CurrentRoadStroke = new();
    [SerializeField] SelectionScheme scheme;

    public override void OnPressEnd(BuildingProperties tile, BuildingType selected)
    {
        CurrentRoadStroke.Clear();
    }
    public override void OnPress(BuildingProperties tile, BuildingType selected)
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
    public override void OnPressStart(BuildingProperties tile, BuildingType selected)
    {
        if (tile.buildingType != BuildingType.None && tile.buildingType != BuildingType.Road && !tile.locked)
        {
            gameManager.RefreshUnitSelectionPanel(tile);
        }
    }

    public bool checkAdjacent(int3 point1, int3 point2)
    {
        if (CurrentRoadStroke.Count == 0) { return true; }
        int dx = Mathf.Abs(point1.x - point2.x);
        int dz = Mathf.Abs(point1.z - point2.z);

        if ((dx == 1 && dz == 0) || (dx == 0 && dz == 1))
        {
            return true;
        }
        return false;
    }

    public void UpdateRoadInStroke(BuildingProperties tile)
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
            int3 previousRoad = CurrentRoadStroke[^2];

            if (new Vector2(gameManager.SelectionGridPos.x - previousRoad.x, gameManager.SelectionGridPos.z - previousRoad.z).magnitude > 1)
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
            else if (gameManager.SelectionGridPos.z > previousRoad.z)
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
            road = gameManager.tileProperties[BuildingUtils.CoordsToSlotID(CurrentRoadStroke[^2], gameManager.gridDimensions)].GetComponent<RoadProperties>();
            roadTable = road.table;

            if (roadTable.up || inverseRoadTable.up) { roadTable.up = true; }
            if (roadTable.down || inverseRoadTable.down) { roadTable.down = true; }
            if (roadTable.left || inverseRoadTable.left) { roadTable.left = true; }
            if (roadTable.right || inverseRoadTable.right) { roadTable.right = true; }

            UpdatetileRoadMaterial(road, roadTable);
        }
    }

    public override void OnTileEnter(BuildingProperties tile, BuildingType selected)
    {

    }

    public override void OnTileExit(BuildingProperties tile, BuildingType selected)
    {

    }

    public override void OnModeEnter(BuildingProperties tile, BuildingType selected)
    {
        gameManager.SetSelectionScheme(scheme);
    }

    public override void OnModeExit(BuildingProperties tile, BuildingType selected)
    {
    }

}
