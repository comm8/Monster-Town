using UnityEngine;
using BuildingTools;

public class TileProperties : MonoBehaviour
{
    public BuildingType buildingType;
    public GameObject model;
    public Transform modelTransform;
    public ushort monsterID = 0;

    private int health;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            //die
        }
    }

}


