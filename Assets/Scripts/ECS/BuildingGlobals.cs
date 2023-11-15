using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using BuildingTools;

public class BuildingGlobals : MonoBehaviour
{
    
}

   /* public class BuildingGlobalsBaker : Baker<BuildingGlobals>
    {
        public override void Bake(BuildingGlobals authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(playerEntity, new PlayerMovementGlobals
            {
                UpSpeed = authoring.UpSpeed,
                ForwardSpeed = authoring.ForwardSpeed,
                Backspeed = authoring.Backspeed,
                SprintMult = authoring.SprintMult,
                CrouchMult = authoring.CrouchMult,
                Friction = authoring.Friction,
                Gravity = authoring.Gravity,
                slopeLimit = authoring.slopeLimit,
                rigidbodyPushForce = authoring.rigidbodyPushForce,
                stepOffset = authoring.stepOffset,
                maxVelocity = authoring.maxVelocity
            });
        }*/
    