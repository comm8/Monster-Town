using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


[Serializable]
public class Attack : ScriptableObject
{
    public float coolDown;


    public virtual void SetUp()
    {

    }
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


    public override void SetUp() { }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
}

[CreateAssetMenu(fileName = "DirectAttack", menuName = "MonsterGame/Attacks/DirectAttack", order = 1)]
public class DirectAttack : Attack
{

    [Space(20)]
    [SerializeField] byte range;

    [SerializeField] byte damage;
    public override void SetUp() { }
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

    public override void SetUp()
    {
        //AOERangeVisual = GameObject.Instantiate()
    }
    override public void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
}
