using UnityEngine;
using Unity.Mathematics;
using BuildingTools;

public class GridInitMono : MonoBehaviour
{
    [SerializeField] Transform tileParent;

    public string[] Names;


    void Start()
    {
        var gameManager = GameManager.instance;

        for (int x = 0; x < gameManager.gridSize; x++)
        {
            for (int y = 0; y < gameManager.gridSize; y++)
            {
                GameObject entity = Instantiate(gameManager.tilePrefab, tileParent);
                entity.transform.position = new float3(y, 0, x) * 10;
                TileProperties tileProperties = entity.GetComponent<TileProperties>();

                tileProperties.buildingType = BuildingType.None;

                tileProperties.model = Instantiate(gameManager.buildings.GetBuilding(BuildingType.None).Model, tileProperties.modelTransform);
                gameManager.tileProperties[x * gameManager.gridSize + y] = tileProperties;
                entity.GetComponentInChildren<TileAnimator>().CacheDeltaPos();
            }


        }
        this.enabled = false;

    }
}
