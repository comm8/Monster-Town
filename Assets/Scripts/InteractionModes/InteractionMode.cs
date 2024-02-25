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
        tile.model = Instantiate(gameManager.modelDictionary.Get(desired), tile.modelTransform);
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

}
