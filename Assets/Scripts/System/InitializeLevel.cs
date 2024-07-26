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



        ushort iterator = 0;

        for (int row = 0; row < gameManager.gridDimensions.x; row++)
        {
            for (int column = 0; column < gameManager.gridDimensions.z; column++)
            {
                for (int pillar = 0; column < gameManager.gridDimensions.y; pillar++)
                {
                    GameObject entity = Instantiate(gameManager.tilePrefab, tileParent);
                    entity.transform.position = new float3(column, pillar, row) * 10;
                    TileProperties tileProperties = entity.GetComponent<TileProperties>();

                    tileProperties.buildingType = BuildingType.None;
                    tileProperties.ID = iterator;
                    tileProperties.model = Instantiate(gameManager.buildings.GetBuilding(BuildingType.None).Model, tileProperties.modelTransform);
                    gameManager.tileProperties[iterator] = tileProperties;
                    entity.GetComponentInChildren<TileAnimator>().CacheDeltaPos();
                    CardManager.instance.CreateSlot(entity.transform, false);
                    iterator++;
                    Debug.Log(iterator);
                }

            }
        }
        gameManager.interaction.OnModeEnter(gameManager.tileProperties[1], BuildingType.Farm);
        enabled = false;
    }
}
