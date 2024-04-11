using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingTools;

[CreateAssetMenu(fileName = "Building", menuName = "MonsterGame/Building", order = 1)]
public class Building : ScriptableObject
{
    public bool randomRotation;

    public ResourceValue[] cost;
    public GameObject Model;

    public MonsterProduction[] production = new MonsterProduction[]{
    new(){ monsterType = MonsterType.Kobold}, new(){ monsterType = MonsterType.Orc},
     new(){ monsterType = MonsterType.Wyvern}, new(){ monsterType = MonsterType.Mindflayer},
      new(){ monsterType = MonsterType.Plantoid}, new(){ monsterType = MonsterType.Skeleton}};

    public Health health;

}
