using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using System.Collections.Generic;
public class RoadInteraction : InteractionMode
{
    [SerializeField] List<int2> CurrentRoadStroke = new();

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        CurrentRoadStroke.Clear();
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None && TryChargeCost(selected))
        {
            if (!checkAdjacent())
            {
                CurrentRoadStroke.Clear();
                tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
                return;
            }
            PlaceTile(tile, BuildingType.Road);
            UpdateRoad(tile);
        }
        else
        {
            if (tile.buildingType == BuildingType.Road)
            {
                UpdateRoad(tile);
            }
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

    public bool checkAdjacent()
    {
        if (CurrentRoadStroke.Count == 0) { return true; }

        var point1 = CurrentRoadStroke[CurrentRoadStroke.Count -1];
        var point2 =  gameManager.SelectionGridPos;

        int dx = Mathf.Abs(point1.x - point2.x);
        int dy = Mathf.Abs(point1.y - point2.y);

        if ((dx == 1 && dy == 0) || (dx == 0 && dy == 1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void UpdateRoad(TileProperties tile)
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

            road.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("road/road_" + BuildingUtils.toNumeralString(roadTable.up) + BuildingUtils.toNumeralString(roadTable.down) + BuildingUtils.toNumeralString(roadTable.left) + BuildingUtils.toNumeralString(roadTable.right));



            //adjust last road
            road = gameManager.tileProperties[BuildingUtils.CoordsToSlotID(CurrentRoadStroke[^2], gameManager.gridSize)].GetComponent<RoadProperties>();
            roadTable = road.table;

            if (roadTable.up || inverseRoadTable.up) { roadTable.up = true; }
            if (roadTable.down || inverseRoadTable.down) { roadTable.down = true; }
            if (roadTable.left || inverseRoadTable.left) { roadTable.left = true; }
            if (roadTable.right || inverseRoadTable.right) { roadTable.right = true; }

            road.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("road/road_" + BuildingUtils.toNumeralString(roadTable.up) + BuildingUtils.toNumeralString(roadTable.down) + BuildingUtils.toNumeralString(roadTable.left) + BuildingUtils.toNumeralString(roadTable.right));
        }
    }






}
