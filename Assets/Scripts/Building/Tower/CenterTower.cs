using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingTools;
using System.Linq;
public class CenterTower : TileProperties
{
    // Start is called before the first frame update
    override public void Damage(DamageType type, byte amount)
    {
        if (health.immunity.Contains(type))
        {
            amount /= 2;
        }

        if (health.weakness.Contains(type))
        {
            amount *= 2;
        }

        hp -= amount;

        if (hp < 1) { Kill(); }
    }
}
