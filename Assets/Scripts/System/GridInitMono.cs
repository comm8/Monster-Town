using UnityEngine;
using Unity.Mathematics;
using BuildingTools;

public class GridInitMono : MonoBehaviour
{
    [SerializeField] Transform tileParent;

    public string[] Names;


    void Start()
    {

        for (int x = 0; x < GameManager.instance.gridSize; x++)
        {
            for (int y = 0; y < GameManager.instance.gridSize; y++)
            {
                GameObject entity = Instantiate(GameManager.instance.tilePrefab, tileParent);
                entity.transform.position = new float3(y, GameManager.instance.heightMap.GetPixel(x, y).r * 10, x) * 10;
                TileProperties tileProperties = entity.GetComponent<TileProperties>();

                tileProperties.buildingType = BuildingTools.BuildingType.None;

                tileProperties.model = Instantiate(GameManager.instance.modelDictionary.Get(BuildingTools.BuildingType.None), tileProperties.modelTransform);
                GameManager.instance.tileProperties[x * GameManager.instance.gridSize + y] = tileProperties;
                entity.GetComponentInChildren<TileAnimator>().CacheDeltaPos();
            }


        }



        this.enabled = false;

    }
}
