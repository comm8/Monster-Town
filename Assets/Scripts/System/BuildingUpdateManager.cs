using UnityEngine;

public class BuildingUpdateManager : MonoBehaviour
{
    public TileProperties[] Tiles;

    void Update()
    {
        foreach (TileProperties tile in Tiles)
        {
            print(tile);
        }
    }
}
