using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using BuildingTools;
using Unity.Mathematics;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BuildingGridInit : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state) {state.RequireForUpdate<BuildingGlobals>();}
    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var buildingGlobals = SystemAPI.GetSingleton<BuildingGlobals>();

        EntityCommandBuffer commandBuffer = new EntityCommandBuffer(Allocator.Temp);

        for (int i = 0; i < math.pow(buildingGlobals.gridSize,2); i++)
        {
            commandBuffer.Instantiate(buildingGlobals.buildingPrefab);
        }
        commandBuffer.Playback(state.EntityManager);
        commandBuffer.Dispose();

    }


}


