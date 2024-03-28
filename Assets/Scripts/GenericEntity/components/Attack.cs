using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Attack : ScriptableObject
{
    public float coolDown;

    public LayerMask mask;

    public virtual void SetUp()
    {

    }

    public virtual void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target) { }
    public virtual void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
}

[CreateAssetMenu(fileName = "RangedAttack", menuName = "MonsterGame/Attacks/RangedAttack", order = 1)]
public class RangedAttack : Attack
{

    [Space(20)]
    [SerializeField] byte range;
    [SerializeField] byte damage;

    [SerializeField] float projectilespeed;


    override public void SetUp() { }

    override public void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target) { }


}

[CreateAssetMenu(fileName = "DirectAttack", menuName = "MonsterGame/Attacks/DirectAttack", order = 1)]
public class DirectAttack : Attack
{

    [Space(20)]
    [SerializeField] byte range;

    [SerializeField] byte damage;
    override public void SetUp() { }
    override public void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {
        if(!Physics.Raycast(AttackerPosition, TargetPosition - AttackerPosition, out RaycastHit hit, range, mask)) {return;}
        
        //WE DID IT!
        //Time to deal damage

        //target.health.



    }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
}

[CreateAssetMenu(fileName = "AOEAttack", menuName = "MonsterGame/Attacks/AOEAttack", order = 1)]
public class AOEAttack : Attack
{

    [Space(20)]
    [SerializeField] float radius;
    [SerializeField] byte damage;


    [SerializeField] private GameObject AOERangeVisual;

    override public void SetUp()
    {
        //AOERangeVisual = GameObject.Instantiate()
    }

    override public void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
}
