using BuildingTools;
using Unity.Mathematics;
using UnityEngine;


public abstract class InteractionMode : MonoBehaviour
{
    [HideInInspector] public GameManager gameManager;
    public abstract void OnPressStart(BuildingData tile, BuildingType selected);
    public abstract void OnPress(BuildingData tile, BuildingType selected);
    public abstract void OnPressEnd(BuildingData tile, BuildingType selected);
    public abstract void OnTileEnter(BuildingData tile, BuildingType selected);
    public abstract void OnTileExit(BuildingData tile, BuildingType selected);
    public abstract void OnModeEnter(BuildingData tile, BuildingType selected);
    public abstract void OnModeExit(BuildingData tile, BuildingType selected);


    public void PlaceTile(BuildingData tile, BuildingType desired)
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
        if (desired == BuildingType.None)
        {
            gameManager.UpdateBitMask(tile.ID, false);
        }
        else
        {
            gameManager.UpdateBitMask(tile.ID, true);
        }
    }


    public void UpdatetileRoadMaterial(RoadProperties road, RoadTable table)
    {
        road.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("road/road_" + table.ToString());
    }

}
