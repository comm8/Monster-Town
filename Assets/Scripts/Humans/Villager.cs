using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : GenericEntity
{
    BehaviorState behavior;

    // Start is called before the first frame update
    void Start()
    {
        behavior = BehaviorState.Idle;
        attack = new AOEAttack();
        attack.SetUp();
        DoAttack();
    }

    void DoAttack()
    {
        behavior = BehaviorState.Attacking;
        attack.Run(transform.position, 20, 20, 3f);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum BehaviorState : byte
{
    Idle,
    Pathfinding,
    Stunned,
    Attacking,
    Grouped
}
