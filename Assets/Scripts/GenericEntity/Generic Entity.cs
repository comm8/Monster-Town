using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingTools;
using System.Linq;

public class GenericEntity : MonoBehaviour
{
    public bool team;
    [SerializeField] byte hp;
    public Health health;
    public Attack attack;


    public void SetUp()
    {
        hp = health.maxHP;
    }


    public void TakeDamage(DamageType type, byte amount)
    {
        if (health.immunity.Contains(type))
        {
            amount /= 2;
        }

        if (health.weakness.Contains(type))
        {
            amount *= 2;
        }

        hp -= amount;

        if(hp < 1) {Kill();}
        }


        public void Kill()
        {

        }
}
