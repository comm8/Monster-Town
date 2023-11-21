using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BuildingSelectorPlaceholder : MonoBehaviour
{

    delegate void RecieveUpdate();
    RecieveUpdate recieveUpdate;

    void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //somehow we need to do a raycast from the position of the mouse to the ECS tiles and get that tile as a reference.
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
