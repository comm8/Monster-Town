using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : GenericEntity
{

    void Awake()
    {
        SetUp();
    }

    void Update()
    {
       var target = attack.TryGetTarget(transform.position);
        if(target != null)
        {
            attack.TryAttack(transform.position, target.GetComponent<Transform>().position, target);
        }
    }
}
