using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingTools;
public class TileProperties : MonoBehaviour
{
    public BuildingType buildingType;
    public MonsterType monsterType;
    public int slotID;

    [Header("PlaceAnimation")]
    [SerializeField] AnimationCurve accelerationCurve;
    float elapsedTime;



}
