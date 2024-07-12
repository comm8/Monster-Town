using UnityEngine;
using Unity.Mathematics;
using BuildingTools;

public class InitializeLevel : MonoBehaviour
{
    public static InitializeLevel instance;
    [SerializeField] Transform tileParent;

    public string[] Names;


    void Start()
    {
        var gameManager = GameManager.instance;
        int iterator = 0;
        for (int x = 0; x < gameManager.gridSize; x++)
        {
            for (int y = 0; y < gameManager.gridSize; y++)
            {

                GameObject entity = Instantiate(gameManager.tilePrefab, tileParent);
                entity.transform.position = new float3(y, 0, x) * 10;
                TileProperties tileProperties = entity.GetComponent<TileProperties>();

                tileProperties.buildingType = BuildingType.None;
                tileProperties.ID = (ushort)iterator;
                tileProperties.model = Instantiate(gameManager.buildings.GetBuilding(BuildingType.None).Model, tileProperties.modelTransform);
                gameManager.tileProperties[x * gameManager.gridSize + y] = tileProperties;
                entity.GetComponentInChildren<TileAnimator>().CacheDeltaPos();
                CardManager.instance.CreateSlot(entity.transform, false);
                iterator++;
            }
        }

        /* var towerProperties = gameManager.tileProperties[BuildingUtils.CoordsToSlotID(new int2(10, 10), gameManager.gridSize)];
        towerProperties.buildingType = BuildingType.Tower;
        towerProperties.locked = true;
        Destroy(towerProperties.model);
        towerProperties.model = Instantiate(gameManager.buildings.buildings[12].Model, towerProperties.modelTransform); */
        gameManager.interaction.OnModeEnter(gameManager.tileProperties[1], BuildingType.Farm);
        this.enabled = false;

    }
}
