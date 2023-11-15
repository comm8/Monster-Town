using BuildingTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
public class BuildingGlobalsBaker : Baker<BuildingGlobalsMono>
    {
        public override void Bake(BuildingGlobalsMono authoring)
        {
            var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(playerEntity, new BuildingGlobals
            {
                gridSize = authoring.gridSize,
                buildingPrefab = GetEntity(authoring.buildingPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }