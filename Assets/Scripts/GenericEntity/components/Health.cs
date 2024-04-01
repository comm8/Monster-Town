using BuildingTools;
using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "HealthScriptableObject", menuName = "MonsterGame/Health", order = 1)]
public class Health : ScriptableObject
{
    public byte maxHP;
    public DamageType[] weakness;
    public DamageType[] immunity;
}
