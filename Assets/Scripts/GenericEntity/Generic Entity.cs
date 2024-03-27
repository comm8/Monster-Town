using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    virtual public void TakeDamage()
    {

    }
}
