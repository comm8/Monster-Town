using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using BuildingTools;
[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]

public partial struct BuildingISystem : ISystem
{


    [BurstCompile]
    public void OnCreate(ref SystemState state) { }
    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }
    [BurstCompile]
    public void RequireForUpdate<BuildingGlobals>() { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
    }


}


[BurstCompile]
public partial struct UpdateBuilding : IJobEntity
{
    [ReadOnly] public BuildingGlobals buildingGlobals;
    public float deltatime;
    static int maxDepth = 20;
    public void Execute()
    {

    }





}
