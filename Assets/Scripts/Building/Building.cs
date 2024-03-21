using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingTools;

[CreateAssetMenu(fileName = "Building", menuName = "MonsterGame/Building", order = 1)]
public class Building : ScriptableObject
{
    public ResourceValue[] cost;
    public GameObject Model;

    public MonsterProduction[] production;

    public Health health;

}
