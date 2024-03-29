using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : GenericEntity
{
    public BehaviorState state = BehaviorState.Pathfinding;
    public bool stunned;
    public byte stunCooldown;

    void Awake()
    {
        SetUp();

        state = BehaviorState.Idle;
        attack.SetUp();
        TryAttack();

        //start: find pathfinding location 
        // if reached target, attack
        // if attacked, stunned
        // if done stunned, repathfind. lower health = more caution
        // if around more powerfull units and high caution, group position

        //We need some type of cluster system
        //Group functionality
    }

    // Update is called once per frame


    void TryAttack()
    {
        state = BehaviorState.Attacking;
       // attack.TryAttack();
    }

    void PathFind()
    {
        


    }

    IEnumerator stun()
    {
        stunned = true;
        yield return new WaitForSeconds(stunCooldown);
        stunned = false;
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
