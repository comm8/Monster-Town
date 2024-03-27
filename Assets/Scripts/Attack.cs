using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


[Serializable]
public abstract class Attack
{

    public abstract void SetUp();
    public abstract void Run(Vector3 position, byte damage, byte spread, float cooldown);
}


public class RangedAttack : Attack
{
    public override void SetUp() { }
    override public void Run(Vector3 position, byte damage, byte spread, float cooldown)
    {

    }
}

public class DirectAttack : Attack
{
    public override void SetUp() { }
    override public void Run(Vector3 position, byte damage, byte spread, float cooldown)
    {

    }
}

public class AOEAttack : Attack
{
    [SerializeField] private GameObject AOERangeVisual; 

    public override void SetUp()
    {
        //AOERangeVisual = GameObject.Instantiate()
    }
    override public void Run(Vector3 position, byte damage, byte spread, float cooldown)
    {

    }
}
