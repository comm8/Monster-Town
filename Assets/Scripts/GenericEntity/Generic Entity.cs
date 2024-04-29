using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingTools;
using System.Linq;
using UnityEditor;

public class GenericEntity : MonoBehaviour
{
    public bool team;
    [SerializeField] protected byte hp;
    public Health health;
    public Attack attack;


    public void SetUp()
    {
        hp = health.maxHP;
    }


    public virtual void Damage(DamageType type, byte amount)
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

        if (hp < 1) { Kill(); }
    }


    public void Kill()
    {

    }



    void OnDrawGizmos()
    {
        if (attack == null) { return; }
        attack.DrawGizmos(transform.position);
    }
}
