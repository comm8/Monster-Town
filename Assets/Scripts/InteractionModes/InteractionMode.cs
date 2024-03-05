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
        if(desired != BuildingType.None)
        {
                    tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
        }
        if(tile.TryGetComponent<RoadProperties>( out RoadProperties road))
        {
            Destroy(road);
        }
    }

  /*   public bool CheckCost( BuildingType desired)
    {
        ResourceValue[]
        ResourceValue[] cost = gameManager.buildings.GetBuilding((int)desired).cost;

        foreach ( ResourceValue debt in cost)
        {
           foreach (ResourceValue resource in gameManager.inventory)
           {
            if(resource.Type == debt.Type)
            {
                if (resource.Amount >= debt.Amount)
                {
                    resource.Amount -= debt.Amount;
                }
                else
                {
                    //CANT AFFORD
                    return false;
                }
            }
           }
        }
    }*/

} 
