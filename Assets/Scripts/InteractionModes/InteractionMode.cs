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
        GameObject desiredModel = gameManager.modelDictionary.Get(desired);

        tile.model = Instantiate(desiredModel, tile.modelTransform);
        tile.buildingType = desired;
        tile.GetComponentInChildren<TileAnimator>().playUpdateAnimation();
    }

}
