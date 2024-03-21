using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public abstract class Attack
{
    public abstract void Run(Vector3 position, byte damage, byte spread, float cooldown);
}

public class FireAttack : Attack
{
    override public void Run(Vector3 position, byte damage, byte spread, float cooldown)
    {

    }
}
