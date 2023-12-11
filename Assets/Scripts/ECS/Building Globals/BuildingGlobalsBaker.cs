using BuildingTools;
using Unity.Burst;
using Unity.Entities;
[BurstCompile]
public class BuildingGlobalsBaker : Baker<BuildingGlobalsMono>
{
    [BurstCompile]
    public override void Bake(BuildingGlobalsMono authoring)
    {
        var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(playerEntity, new BuildingGlobals
        {
            gridSize = authoring.gridSize,
            buildingPrefab = GetEntity(authoring.buildingPrefab, TransformUsageFlags.Dynamic),
            farmResourceTable = authoring.resourceGlobals.farmResourceTable,
            lumber_YardResourceTable = authoring.resourceGlobals.farmResourceTable,
            mineResourceTable = authoring.resourceGlobals.farmResourceTable,
            innResourceTable = authoring.resourceGlobals.farmResourceTable,
            forgeResourceTable = authoring.resourceGlobals.farmResourceTable,
            necroMansionResourceTable = authoring.resourceGlobals.farmResourceTable,
            fishing_DockResourceTable = authoring.resourceGlobals.farmResourceTable,
            light_HouseResourceTable = authoring.resourceGlobals.farmResourceTable,
            apothecaryResourceTable = authoring.resourceGlobals.farmResourceTable,
            armoryResourceTable = authoring.resourceGlobals.farmResourceTable
        });
    }
}