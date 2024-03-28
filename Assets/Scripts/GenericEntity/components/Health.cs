using BuildingTools;
using UnityEngine;


[CreateAssetMenu(fileName = "HealthScriptableObject", menuName = "MonsterGame/Health", order = 1)]
public class Health : ScriptableObject
{
    public byte maxHP;
    public DamageType[] weakness;
    public DamageType[] immunity;

    public void TakeDamage(DamageType type, byte amount)
    {
        
    }

}
