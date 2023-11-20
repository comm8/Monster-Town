using BuildingTools;

using Unity.Entities;
public class BuildingGlobalsBaker : Baker<BuildingGlobalsMono>
{
    public override void Bake(BuildingGlobalsMono authoring)
    {
        var playerEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(playerEntity, new BuildingGlobals
        {
            gridSize = authoring.gridSize,
            buildingPrefab = GetEntity(authoring.buildingPrefab, TransformUsageFlags.Dynamic),
            finishedEntityCreation = authoring.finishedEntityCreation
        });
    }
}