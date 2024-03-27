using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : GenericEntity
{
    public HumanState state = HumanState.Pathfinding;


    // Start is called before the first frame update
    void Start()
    {
       //start: find pathfinding location 
       // if reached target, attack
       // if attacked, stunned
       // if done stunned, repathfind. lower health = more caution
       // if around more powerfull units and high caution, group position

       //We need some type of cluster system
       //Group functionality
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PathFind()
    {
        


    }


}

public enum HumanState : byte
{
Pathfinding,
Stunned,
Attacking
}
