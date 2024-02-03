using UnityEngine;
using Unity.Mathematics;
using BuildingTools;

public class GridInitMono : MonoBehaviour
{
    [SerializeField] Transform tileParent;  

    public string[] Names;


    void Start()
    {

        for (int i = 0; i < math.pow(GameManager.instance.gridSize, 2); i++)
        {
            int k = i % GameManager.instance.gridSize;
            GameObject entity = Instantiate(GameManager.instance.tilePrefab, tileParent);
            entity.transform.position = new float3(k, 0, i / GameManager.instance.gridSize) * 10;
          TileProperties tileProperties =  entity.GetComponent<TileProperties>();

            tileProperties.buildingType = BuildingTools.BuildingType.None;

            tileProperties.model = Instantiate(GameManager.instance.modelDictionary.Get(BuildingTools.BuildingType.None), tileProperties.modelTransform);
            GameManager.instance.tileProperties[i] = tileProperties;
            entity.GetComponentInChildren<TileAnimator>().CacheDeltaPos();
        }
        
        
         GameManager.instance.monsters = new();
    for(int i = 0; i > 50; i++)
    {
        GameManager.instance.monsters.Add  (new MonsterStats{name = Names[UnityEngine.Random.Range(0,99)],});
    }
        this.enabled = false;

    }
}
