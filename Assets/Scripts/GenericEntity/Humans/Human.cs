using System.Collections;
using UnityEngine;
using BuildingTools;
using Unity.Mathematics;

public class Human : GenericEntity
{
    public BehaviorState state = BehaviorState.Pathfinding;
    public bool stunned;
    public byte stunCooldown;
    private int2 coordPos;
    private int2 targetPos;
    private byte[] directions;

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
        var target = attack.TryGetTarget(transform.position);
        if (target != null)
        {
            attack.TryAttack(transform.position, target.GetComponent<Transform>().position, target);
        }

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