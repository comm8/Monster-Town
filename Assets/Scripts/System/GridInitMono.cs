using UnityEngine;
using Unity.Mathematics;
using Unity.Burst;

public class GridInitMono : MonoBehaviour
{
    [SerializeField] Transform tileParent;  

    void Start()
    {


        for (int i = 0; i < math.pow(GameManager.instance.gridSize, 2); i++)
        {
            int k = i % GameManager.instance.gridSize;
            GameObject entity = Object.Instantiate(GameManager.instance.tilePrefab, tileParent);
            entity.transform.position = new float3(k, 0, i / GameManager.instance.gridSize) * 10;

          TileProperties tileProperties =  entity.AddComponent<TileProperties>();
            GameManager.instance.tileProperties[i] = tileProperties;

            this.enabled = false;
        }
    }
}
