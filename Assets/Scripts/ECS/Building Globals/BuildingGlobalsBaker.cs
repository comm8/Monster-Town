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
        });
    }
}