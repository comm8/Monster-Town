using BuildingTools;
using Unity.Mathematics;
using UnityEngine;


public abstract class InteractionMode : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;
    public abstract void OnPressStart(TileProperties tile, BuildingType selected);
    public abstract void OnPress(TileProperties tile, BuildingType selected);
    public abstract void OnPressEnd(TileProperties tile, BuildingType selected);
    public abstract void OnTileEnter(TileProperties tile, BuildingType selected);
        public abstract void OnTileExit(TileProperties tile, BuildingType selected);
    public abstract void OnModeEnter(TileProperties tile, BuildingType selected);
    public abstract void OnModeExit(TileProperties tile, BuildingType selected);


    public void PlaceTile(TileProperties tile, BuildingType desired)
    {
        Destroy(tile.model);

        tile.model = Instantiate(gameManager.buildings.GetBuilding(desired).Model, tile.modelTransform);

        if (gameManager.buildings.GetBuilding(desired).randomRotation)
        {
            tile.model.transform.Rotate(0, 90 * UnityEngine.Random.Range(0, 4), 0);
        }
        else
        {
            tile.model.transform.rotation = quaternion.identity;
        }

        tile.buildingType = desired;
        if (tile.TryGetComponent(out RoadProperties road))
        {
            Destroy(road);
        }
    }


    public void UpdatetileRoadMaterial(RoadProperties road, RoadTable table)
    {
        road.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("road/road_" + table.ToString());
    }

}
