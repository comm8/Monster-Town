using UnityEngine;
using Unity.Mathematics;
using BuildingTools;

public class InitializeLevel : MonoBehaviour
{
    public static InitializeLevel instance;
    [SerializeField] Transform tileParent;



    void Start()
    {
        var gameManager = GameManager.instance;




        for (ushort iterator = 0; iterator < gameManager.gridSize; iterator++)
        {
                    GameObject entity = Instantiate(gameManager.tilePrefab, tileParent);
                    int3 coords = BuildingUtils.SlotIDToCoords(iterator, gameManager.gridDimensions);
                    entity.transform.position = new Vector3(coords.x,coords.y,coords.z) * 10;
                    BuildingProperties tileProperties = entity.GetComponent<BuildingProperties>();

                    tileProperties.buildingType = BuildingType.None;
                    tileProperties.ID = iterator;
                    tileProperties.model = Instantiate(gameManager.buildings.GetBuilding(BuildingType.None).Model, tileProperties.modelTransform);
                    gameManager.tiles[iterator] = tileProperties;
                    entity.GetComponentInChildren<TileAnimator>().CacheDeltaPos();
                    CardManager.instance.CreateSlot(entity.transform, false);
        }
        gameManager.interaction.OnModeEnter(gameManager.tiles[1], BuildingType.Farm);
        enabled = false;
    }
}
