using System;
using BuildingTools;
using UnityEngine;


[Serializable]
public class Attack : ScriptableObject
{

    public DamageType type;
    public float coolDown;

    public LayerMask mask;

    public virtual void SetUp()
    {

    }




    public virtual void DrawGizmos(Vector3 position)
    {

    }

    public virtual GenericEntity TryGetTarget(Vector3 position) { return null; }
    public virtual void TryAttack(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target) 
    {
    }
    public virtual void Run(Vector3 AttackerPosition, Vector3 TargetPosition, GenericEntity target)
    {

    }
}



