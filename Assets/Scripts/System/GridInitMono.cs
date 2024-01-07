using UnityEngine;
using Unity.Mathematics;

public class GridInitMono : MonoBehaviour
{
    [SerializeField] Transform tileParent;  

    void Start()
    {


        for (int i = 0; i < math.pow(GameManager.instance.gridSize, 2); i++)
        {
            int k = i % GameManager.instance.gridSize;
            GameObject entity = Instantiate(GameManager.instance.tilePrefab, tileParent);
            entity.transform.position = new float3(k, 0, i / GameManager.instance.gridSize) * 10;
          TileProperties tileProperties =  entity.GetComponent<TileProperties>();
            tileProperties.model = Instantiate(GameManager.instance.modelList.FarmModel, tileProperties.modelTransform);
            GameManager.instance.tileProperties[i] = tileProperties;

            this.enabled = false;
        }
    }
}
