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
        attack = new AOEAttack();
        attack.SetUp();
        DoAttack();

        //start: find pathfinding location 
        // if reached target, attack
        // if attacked, stunned
        // if done stunned, repathfind. lower health = more caution
        // if around more powerfull units and high caution, group position

        //We need some type of cluster system
        //Group functionality
    }

    // Update is called once per frame


    override public void TakeDamage()
    {
        //take damage, if health < 1, kill.
        //Else, stun
    }

    void DoAttack()
    {
        state = BehaviorState.Attacking;
        //attack.Run(transform.position, 20, 20, 3f);
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
