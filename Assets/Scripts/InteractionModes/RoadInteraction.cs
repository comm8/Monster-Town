using UnityEngine;
using BuildingTools;
using Unity.Mathematics;
using System.Collections.Generic;
public class RoadInteraction : InteractionMode
{
   [SerializeField] List<int2> CurrentRoadStroke = new();

    public override void OnPressEnd(TileProperties tile, BuildingType selected)
    {
        CurrentRoadStroke = new();
    }
    public override void OnPress(TileProperties tile, BuildingType selected)
    {
        if (tile.buildingType == BuildingType.None)
        {
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
