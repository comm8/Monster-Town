using System.Collections;
using System.Collections.Generic;
using BuildingTools;
using UnityEngine;


public abstract class InteractionMode : MonoBehaviour
{

    public GameManager gameManager;

    public abstract void OnPressStart(TileProperties tile, BuildingType selected);
    public abstract void OnPress(TileProperties tile, BuildingType selected);
    public abstract void OnPressEnd(TileProperties tile, BuildingType selected);


    public void PlaceTile(TileProperties tile, BuildingType desired)
    {
        Destroy(tile.model);
        tile.model = Instantiate(gameManager.buildings.GetBuilding(desired).Model, tile.modelTransform);
        tile.buildingType = desired;
        if (desired != BuildingType.None)
        {
            tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }
        if (tile.TryGetComponent<RoadProperties>(out RoadProperties road))
        {
            Destroy(road);
        }
    }

    public bool TryChargeCost(BuildingType desired)
    {
        List<int> cache = new();
        ResourceValue[] cost = gameManager.buildings.GetBuilding((int)desired).cost;

        foreach (ResourceValue debt in cost)
        {
            for (int i = 0; i < gameManager.inventory.Length; i++)
            {
                var resource = gameManager.inventory[i];

                if (resource.Type == debt.Type)
                {
                    if (resource.Amount >= debt.Amount)
                    {
                        cache.Add(i);
                    }
                    else
                    {
                        //CANT AFFORD
                        return false;
                    }
                }
            }
        }

        //We can afford it!! 

        for( int i = 0; i < cache.Count; i++)
        {
            gameManager.inventory[cache[i]].Amount -= cost[i].Amount;
        }

        return true;
    }

        public void UpdatetileRoadMaterial(RoadProperties road, RoadTable table)
    {
        road.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("road/road_" + table.ToString());
    }

}
